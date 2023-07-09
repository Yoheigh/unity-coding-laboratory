using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCategoryUI : MonoBehaviour
{
    public int ObjectCount = 4;
    public float DefaultX = 0f;
    public float DefaultY = 0f;
    public float DefaultRadian = 30f;
    public float Offset = 20f;

    public GameObject UIElement;
    public GameObject Canvas;

    // ���� ����
    private int tempCount;
    private int CountLimit = 33;

    private List<GameObject> objects = new List<GameObject>();

    private void Start()
    {
        DefaultX = Camera.main.pixelWidth / 2;
        DefaultY = Camera.main.pixelHeight / 2;
    }

    void Update()
    {
        if(ObjectCount > 0 && ObjectCount != tempCount && ObjectCount < CountLimit)
        {
            Debug.Log("������ �ٲߴϴ�.");
            foreach (var item in objects)
            {
                Destroy(item);
            }

            objects.Clear();

            for (int i = 0; i < ObjectCount; i++)
            {
                float angle = i * 2 * Mathf.PI / ObjectCount; // ���� ������ ���� ���
                Debug.Log($"{i}��° : {angle * Mathf.Rad2Deg}");
                float startX = DefaultX + DefaultRadian * Mathf.Cos(angle + (Offset * Mathf.Deg2Rad));
                float startY = DefaultY + DefaultRadian * Mathf.Sin(angle + (Offset * Mathf.Deg2Rad));

                var uiObj = Instantiate<GameObject>(UIElement, new Vector3(startX, startY, 0f), Quaternion.identity, Canvas.transform);
                uiObj.name = $"{i}��° ������Ʈ";
                objects.Add(uiObj);
            }

            tempCount = ObjectCount;
        }
        else if(ObjectCount >= CountLimit)
        {
            Debug.Log("���� ������ �ʹ� �����ϴ�!");
        }
    }
}
