using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomStick : MonoBehaviour
{
    public float MoveLimit = 50f;

    private ExpressProject input;

    // 내부 변수
    private float defaultPosX;
    private float defaultPosY;
    private float addPosX;
    private float addPosY;

    private void Awake()
    {
        input = new ExpressProject();

        defaultPosX = transform.position.x;
        defaultPosY = transform.position.y;

        input.Player.Move.Enable();
    }

    private void Start()
    {
        StartCoroutine("TestCO");
    }

    void Update()
    {
        UpdateStickPos();
    }

    void UpdateStickPos()
    {
        addPosX = input.Player.Move.ReadValue<Vector2>().x;
        addPosY = input.Player.Move.ReadValue<Vector2>().y;

        Vector3 newPos = new Vector3(defaultPosX + addPosX * MoveLimit, defaultPosY + addPosY * MoveLimit, 0f);

        transform.position = newPos;
    }

    IEnumerator TestCO()
    {
        Debug.Log($"{addPosX},{addPosY} : 현재 벡터 위치");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("TestCO");
    }
}
