using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Question02 : MonoBehaviour
{
    public string[] baseInputs = new string[2]
    {
        "20",
        "4",
    };

    public string[] itemPropertyInputs = new string[]
{
        "3 4 10 3",
        "4 1 4 2",
        "10 2 5 5",
        "15 1 3 7"
};

    int[] baseInputsInt;
    int[] itemPropertyInputsInt;

    private int x;
    private int itemCount;

    const int speed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    public int[] StringArrayToIntArray(string[] strings)
    {
        int[] toInts = new int[strings.Length];
        for (int i = 0, length = strings.Length; i < length; i++)
        {
            if (int.TryParse(strings[i], out int result))
            {
                toInts[i] = result;
            }
            else
            {
                Debug.LogError($"{i}��°�� ����(int)�� �ű� �� ���� ���ڿ��� ���ԵǾ� �ֽ��ϴ�.");
                return null;
            }
        }

        return toInts;
    }

    void Main(string input)
    {
        int result;

        // base
        for (int i = 0, length = baseInputs.Length; i < length; i++)
        {
            switch (i)
            {
                case 0:
                    int.TryParse(baseInputs[i], out result);
                    x = result;
                    break;
                case 1:
                    int.TryParse(baseInputs[i], out result);
                    itemCount = result;
                    break;
            }
        }

        // itemProperty
        for (int i = 0, length = itemPropertyInputs.Length; i < length; i++)
        {
            int[] itemProperty = itemPropertyInputs[i].Split(' ');
            // ����ó�� �ʿ�

            int a;  // �Ź��� �����Ǳ������ �ð�
            int b;  // �Ź��� �����ϱ������ �ð�
            int c;  // �Ź��� ���ӽð�
            int d;  // �Ź��� c�ʵ��� �޸� �� �ִ� �ӵ�

            //for (int j = 0; j < 4; j++)
            //{

            //}
        }
    }

    public void CompareSpeed(int[] itemProperty)
    {
        int a = itemProperty[0];  // �Ź��� �����Ǳ������ �ð�
        int b = itemProperty[1]; // �Ź��� �����ϱ������ �ð�
        int c = itemProperty[2]; // �Ź��� ���ӽð�
        int d = itemProperty[3];  // �Ź��� c�ʵ��� �޸� �� �ִ� �ӵ�


    }
}
