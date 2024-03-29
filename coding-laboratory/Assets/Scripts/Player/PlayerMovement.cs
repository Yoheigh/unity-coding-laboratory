﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Default State 플레이어 설정")]
    [Tooltip("걸을 때, 1초에 moveSpeed 미터만큼 이동합니다.")]
    public float MoveSpeed = 2.0f;

    [Tooltip("달릴 때, 1초에 sprintSpeed 미터만큼 이동합니다.")]
    public float SprintSpeed = 3.533f;

    [Tooltip("캐릭터의 회전 속도를 조절합니다. 낮을 수록 빠르게 회전합니다.")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.03f;

    [Tooltip("애니메이션 가속력을 정의합니다.")]
    public float SpeedChangeRate = 10.0f;

    [Tooltip("미끄러운 바닥에서의 마찰력을 정의합니다.")]
    public float FrictionSpeed = 1.0f;

    //[Tooltip("점프할 때, jumpHeight 블럭 만큼 점프합니다.")]
    //public float jumpHeight = 1.2f;

    //[Tooltip("다시 점프할 수 있을 때까지 jumpTimeout 초 걸립니다.")]
    //public float jumpTimeout;

    [Tooltip("공중에서 추락 상태까지 fallTimeout 초 걸립니다.")]
    public float FallTimeout = 0.15f;

    [Tooltip("중력을 조절합니다. 낮을 수록 빠르게 떨어집니다.")]
    public float GravityValue = -10.0f;

    [Tooltip("낙하 데미지를 받을 최소 높이를 정합니다.")]
    public float FallDamageHeight = 6.0f;

    [Header("바닥 인식")]
    [Tooltip("바닥 인식을 활성화합니다.")]
    public bool isGround = true;

    [Tooltip("캐릭터 중심으로부터 y축으로 groundCheckOffset 만큼 멀어집니다.")]
    public float GroundCheckOffset = -0.15f;

    [Tooltip("바닥을 인식할 구체의 반지름입니다.")]
    public float GroundCheckRadius = 0.2f;

    [Tooltip("바닥으로 인식할 Layer입니다. (기본 : Block)")]
    public LayerMask GroundLayers;

    // 내부 변수
    private float targetRotation;
    private float rotationVelocity;
    private float verticalVelocity;
    private float fallTimeoutDelta;
    private readonly float terminalVelocityMax = 20.0f;
    private readonly float terminalVelocityMin = -10.0f;
    private Vector3 currentPos;         // 사다리, 잡기 등의 플레이어 현재 위치값
    private Vector3 lastPos;            // 사다리, 잡기 등의 플레이어 고정 위치값
    private float maxReachPoint;        
    private float verticalSnap;
    private Vector3 slipperyDirection;  // 미끄러지는 땅에서의 방향 벡터
    private float fallStartHeight;      // 떨어지기 시작한 높이
    private float tempHeight;           // 얼마나 낙하했는가
    private bool isFallDamageActivated; // 다음 번에 땅에 닿으면 낙하 데미지
    private Vector3 forceVector;        // 외부에서 힘을 줄 때 사용하는 벡터

    // 미끄러지는 상태
    public bool isSlippery = false;
    private float slipperySpeed;

    // 바닥 인식할 구(Sphere) 시작점
    private Vector3 spherePosition;

    // 애니메이션 값
    public int animIDSpeed;
    public float animationBlend;
    public int animIDHoldMove;
    public int animIDMotionSpeed;

    private CharacterController characterController;

    public GameObject MainCamera;
    public Animator animator;

    public void Setup()
    {
        if (MainCamera == null)
            MainCamera = Camera.main.gameObject;

        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        AssignAnimationIDs();
        fallTimeoutDelta = FallTimeout;
        slipperySpeed = 0.0f;

        // LayerMask는 한 번에 여러 레이어를 담을 수 있다는 사실을 몰랐다
        // 비트 반전을 이용해 Entity를 제외한 모든 레이어를 땅으로 인식하게 한다
        GroundLayers = ~LayerMask.GetMask("Entity");

        Debug.Log($"2. Setup - {this}");
    }

    public void InitTest()
    {
        fallTimeoutDelta = FallTimeout;
        currentPos = Vector3.zero;
        lastPos = Vector3.zero;
        Debug.Log("3 - 벡터 초기화");
    }

    public void AssignAnimationIDs()
    {
        animIDSpeed = Animator.StringToHash("Speed");
        animIDHoldMove = Animator.StringToHash("Direction");
        //animIDGrounded = Animator.StringToHash("Grounded");
        //animIDJump = Animator.StringToHash("Jump");
        //animIDFreeFall = Animator.StringToHash("FreeFall");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    public void GroundCheck()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y - GroundCheckOffset,
                                    transform.position.z);

        isGround = Physics.CheckSphere(spherePosition, GroundCheckRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }

    // State에서 벗어났을 때 낙하 데미지를 입는지
    public void FallDamageCheck()
    {
        if(isFallDamageActivated)
        {
            //controller.Hit();
            isFallDamageActivated = false;
        }
    }

    // 다른 상태 변수들을 체크하고 다음 State가 Default인지 확인하는 함수
    public bool IsNextStateDefault()
    {
        if (isFallDamageActivated)
            return false;

        return true;
    }

    // 중력 적용
    public void Gravity()
    {
        // 바닥 감지
        GroundCheck();

        // 바닥을 감지했다면
        if (isGround)
        {
            fallStartHeight = transform.position.y;
            tempHeight = 0;

            fallTimeoutDelta = FallTimeout;

            if (verticalVelocity <= 0.0f)
                verticalVelocity = -1.5f;
        }
        else
        {
            verticalVelocity += GravityValue * Time.deltaTime;

            if (fallStartHeight < transform.position.y)
                fallStartHeight = transform.position.y;

            tempHeight = Mathf.Abs(fallStartHeight - transform.position.y);
            Debug.Log($"추락한 높이 : {tempHeight} 블럭");

            // 추락한 높이가 데미지 높이보다 높다면
            if (tempHeight >= FallDamageHeight)
            {
                // 다음에 땅에 닿을 경우 데미지 입도록 처리
                if (!isFallDamageActivated)
                {
                    Debug.Log("************* 추락시 낙뎀 적용 *************");
                    isFallDamageActivated = true;
                }
            }
        }

        // 플레이어가 공중으로 상승하는 속도 제한
        if (verticalVelocity > terminalVelocityMax) 
            verticalVelocity = terminalVelocityMax;

        // 중력 최저치 이하일 경우 최저치로 고정
        else if (verticalVelocity < terminalVelocityMin)
            verticalVelocity = terminalVelocityMin;

        // 중력 적용
        characterController.Move(new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    // Default -> Falling 체크
    public void GravityToFallCheck()
    {
        // 추락 상태까지 유예 시간
        if (fallTimeoutDelta >= 0.0f)
        {
            fallTimeoutDelta -= Time.deltaTime;
        }
        else // 추락 시작
        {
           
        }
    }
    
    // WASD 조작
    public void Move()
    {

        //animator.SetFloat(animIDSpeed, animationBlend);
        //animator.SetFloat(animIDMotionSpeed, inputMagnitude);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, GroundCheckRadius);
    }
}