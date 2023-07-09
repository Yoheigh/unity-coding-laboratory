using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCategoryUI : MonoBehaviour
{
    public int ObjectCount = 4;
    public float DefaultX = 0f;
    public float DefaultY = 0f;
    public float DefaultRadian = 30f;

    // ���� ����
    private int tempCount;
    private int CountLimit = 33;

    private List<GameObject> objects = new List<GameObject>();

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
                float startX = DefaultX + DefaultRadian * Mathf.Cos(angle);
                float startY = DefaultY + DefaultRadian * Mathf.Sin(angle);

                GameObject uiObj = Instantiate(new GameObject(), new Vector3(startX, startY, 0f), Quaternion.identity);
                uiObj.name = $"{ObjectCount}��° ������Ʈ";
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
