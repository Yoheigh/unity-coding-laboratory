using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    public Manager Manager => Manager.Instance;

    void Start()
    {
        Manager.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                StartCoroutine(StartGen());
            }
        });    
    }

    IEnumerator StartGen()
    {
        List<BoxController> bxList = new List<BoxController>();
        for (int i = 0; i < 50; i++)
        {
            /* �񵿱� ó���� �����ð��� �ִ� ó�� ����̶�� �� ȥ�� �����ؼ� ���͵��ϴٰ� ���� �Լ� */

            // �Լ� �ϳ��� ��� ó���� �� �ϴ� ���   -> ���������� ���� ���� �����ϱ� ������ ���� ó��
            // �ݹ����� �ٸ� �Լ��� �θ��� ���       -> A �Լ����� B �Լ��� ������ �д��� �� �ֱ� ������ �񵿱� ó��
            // ���� �ݹ��� �񵿱� ó�� ��� �� �ϳ�

            /* JavaScript ���ǿ��� �� �׷��� �ݹ� ��Ⱑ ���� ������ �ߴ���... */
            Manager.UI.UpdateLoadingUI(i + 1, 50);

            Vector3 RVector3 = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            BoxController bx = Manager.Object.Spawn<BoxController>(RVector3);
            bxList.Add(bx);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < 50; i++)
        {
            Manager.UI.UpdateLoadingUI(i + 1, 50);
            Manager.Object.Despawn<BoxController>(bxList[i]);
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(StartGen());
    }
}
