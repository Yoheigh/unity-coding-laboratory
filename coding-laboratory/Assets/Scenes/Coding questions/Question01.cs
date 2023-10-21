using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Question01 : MonoBehaviour
{
    string input;
    int answer;

    void Start()
    {
        Main("20");
        Main("630");
        Main("5");
        Main("25");
        Main("214000");
        Main("0");
    }

    public void Main(string input)
    {
        Stopwatch stopwatch = new Stopwatch();

        answer = 0;

        if (input == "0")
        {
            UnityEngine.Debug.Log("���ڿ� �ν��� �����մϴ�.");
            return;
        }

        int.TryParse(input, out int n);

        stopwatch.Start();

        for (int i = 1; i < n; i++)
        {
            int temp = n - (n - i);
            long number = (long)temp * temp;

            // F(x) ���� üũ
            string tempStr = temp.ToString();
            string numberStr = number.ToString();

            numberStr = numberStr.Remove(0, numberStr.Length - tempStr.Length);

            if (numberStr == tempStr)
            {
                // return 1;
                answer = temp;
            }
            else
            {
                // return 0;
            }
        }

        stopwatch.Stop();
        UnityEngine.Debug.Log($"�Ҹ�ð� : {stopwatch.ElapsedMilliseconds}\n{input}���� ���� ������ ������ �����ϴ� ���� ū �� : {answer}");
        stopwatch.Reset();
    }
}
