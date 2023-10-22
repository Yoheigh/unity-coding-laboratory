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
                Debug.LogError($"{i}번째에 정수(int)로 옮길 수 없는 문자열이 포함되어 있습니다.");
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
            // 예외처리 필요

            int a;  // 신발이 생성되기까지의 시간
            int b;  // 신발을 장착하기까지의 시간
            int c;  // 신발의 지속시간
            int d;  // 신발의 c초동안 달릴 수 있는 속도

            //for (int j = 0; j < 4; j++)
            //{

            //}
        }
    }

    public void CompareSpeed(int[] itemProperty)
    {
        int a = itemProperty[0];  // 신발이 생성되기까지의 시간
        int b = itemProperty[1]; // 신발을 장착하기까지의 시간
        int c = itemProperty[2]; // 신발의 지속시간
        int d = itemProperty[3];  // 신발의 c초동안 달릴 수 있는 속도


    }
}
