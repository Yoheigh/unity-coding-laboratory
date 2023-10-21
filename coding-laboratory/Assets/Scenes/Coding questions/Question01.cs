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
            UnityEngine.Debug.Log("문자열 인식을 종료합니다.");
            return;
        }

        int.TryParse(input, out int n);

        stopwatch.Start();

        for (int i = 1; i < n; i++)
        {
            int temp = n - (n - i);
            long number = (long)temp * temp;

            // F(x) 조건 체크
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
        UnityEngine.Debug.Log($"소모시간 : {stopwatch.ElapsedMilliseconds}\n{input}보다 작은 수에서 조건을 만족하는 가장 큰 수 : {answer}");
        stopwatch.Reset();
    }
}
