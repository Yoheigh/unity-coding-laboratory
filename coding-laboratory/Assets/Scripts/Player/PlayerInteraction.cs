using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    // 인터랙션 타겟이 변경될 때 호출하는 이벤트
    public Action<InteractionObject> OnTargetChange;

    private bool isInteracting = false;
    public bool IsInteracting
    {
        get => isInteracting;
        private set => isInteracting = value;
    }

    private InteractionObject interactionObj;
    public InteractionObject InteractionObj     // 다른 곳에서 필요한 경우 사용할 것
    {
        get => interactionObj;
        set => interactionObj = value;
    }

    [SerializeField]
    private FOVSystem fov;
    private PlayerController controller;

    private bool isBuildMode = false;

    // 내부 변수
    private Vector3 equipActionPos;
    private WaitForSeconds equipWaitTime;

    public void Setup()
    {
        // fov = GetComponent<FOVSystem>();
        controller = GetComponent<PlayerController>();

        Debug.Log($"Setup - {this}");
    }

    private void Update()
    {
        equipActionPos = transform.position + transform.forward * 0.6f;
    }

    public void Interact()
    {
        if (isInteracting) return;

        // 손에 무언가 들고 있는 상태일 경우 처리
        if (interactionObj != null)
        {
            // 오브젝트의 enum에 따라서 플레이어 상태 변경
            switch (interactionObj.ObjectType)
            {
                case ObjectType.Grabable:
                    // controller.anim.ChangeAnimationState("Throw");
                    // 던지기 코드 처리
                    /* 지금은 그냥 보여주기용 */
                    controller.animator.SetBool("isLifting", false);
                    ReleaseGrabable(interactionObj);
                    break;

                case ObjectType.Dragable:
                    controller.ChangeState(PlayerStateType.Default);
                    break;
            }

            interactionObj = null;
            return;
        }

        // 건물 생성 모드
        //if (isBuildMode)
        //{
        //    Manager.Instance.Build.Construct();
        //    isBuildMode = false;
        //    return;
        //}

        // 근처에 상호작용 가능한 오브젝트가 없을 경우 종료
        if (!NearObjectCheck()) return;

        // var temp = fov.ClosestTransform.GetComponent<InteractionObject>();

        if (fov.ClosestTransform.TryGetComponent(out InteractionObject temp))
        {
            // 오브젝트의 인터랙션 기능이 있으면 활성화
            temp.InteractWithPlayer(controller);

            // 오브젝트의 enum에 따라서 플레이어 상태 변경
            switch (temp.ObjectType)
            {
                case ObjectType.Grabable:
                    interactionObj = temp;
                    controller.animator.SetBool("isLifting", true);
                    StartCoroutine(ObjectMoveToOverhead(interactionObj));
                    break;

                case ObjectType.Dragable:
                    interactionObj = temp;
                    StartCoroutine(GrabDragable((Dragable)interactionObj));
                    break;

                case ObjectType.Pickup:
                    StartCoroutine(PickUpDelay());
                    break;

                case ObjectType.StageObject:
                    Debug.Log($"{temp}와 상호작용");
                    break;
            }
        }
        else
            Debug.Log("LayerMask : Interactable 이지만 <InteractionObject> 컴포넌트가 없습니다");
    }

    // 인터랙션이 가능한지 체크하는 함수
    // 선택된 오브젝트가 바뀔 때 이벤트를 호출할 수도 있음
    // 현재는 예외 없이 근처에 상호작용 블럭이 있는지만 체크
    public bool NearObjectCheck()
    {
        if (fov.ClosestTransform != null)
        {
            return true;
        }
        return false;
    }

    public void InteractWithEquipment()
    {
        if (isInteracting) return;

        //isInteracting = true;

        //switch (currentEquipment.EquipActionType)
        //{
        //    case EquipmentActionType.Melee:
        //        MeleeAction();
        //        break;

        //    case EquipmentActionType.Ranged:
        //        RangedAction();
        //        break;
        //}
    }

    // 근접 도구 함수
    public void MeleeAction()
    {
        // 애니메이션 재생
        // StartCoroutine(MeleeActionDelay());
    }

    // 원거리 도구 함수
    public void RangedAction() { }

    // 가까이 있는 오브젝트가 바뀔 때마다 작동시킬 Action
    /* 지금은 함수 등록도, 사용도 안 함 */
    public void UpdateInteractChange()
    {
        var temp = fov.ClosestTransform;

        if (temp != fov.ClosestTransform)
        {
            OnTargetChange?.Invoke(fov.ClosestTransform.GetComponent<InteractionObject>());
        }
    }

    // Sine 함수 콜백 ( 점차 증가하는 속도가 빨라지는 그래프 )
    public void GraphSine(float lerpTime, Action<float> callback = null)
    {
        StartCoroutine(GraphSineCoroutine(lerpTime, callback));
    }

    private IEnumerator GraphSineCoroutine(float lerpTime, Action<float> callback = null)
    {
        float currentTime = 0f;
        float t;

        while (currentTime < lerpTime)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= lerpTime)
                currentTime = lerpTime;

            t = currentTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            // 콜백 실행
            callback?.Invoke(t);
            yield return null;
        }
    }
}
