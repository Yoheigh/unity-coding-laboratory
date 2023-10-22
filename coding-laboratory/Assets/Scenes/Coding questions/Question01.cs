using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class Question01 : MonoBehaviour
{
    public string input;
    int answer;
    int index;
    int run;

    int temp;
    long number;

    string tempStr;
    string numberStr;
    string check;

    void Start()
    {
        Main(input);
        //Main("20");
        //Main("630");
        //Main("5");
        //Main("25");
        //Main("2140000000");
        //Main("0");
    }

    public void Main(string input)
    {
        Stopwatch stopwatch = new Stopwatch();

        answer = 0;
        index = 0;
        run = 0;

        if (input == "0")
        {
            UnityEngine.Debug.Log("문자열 인식을 종료합니다.");
            return;
        }

        int.TryParse(input, out int n);

        stopwatch.Start();

        //for(int decimalCount = 1, max = int.MaxValue; decimalCount > max; decimalCount++)
        //{
        //    int decimalCheck = 10 * (10 * decimalCount);
        //    if(n % decimalCheck < decimalCheck)
        //    {

        //    }
        //}

        for (int index = 1; index < n; index++)
        {
            temp = n - (n - index);
            number = (long)temp * temp;
            numberStr = number.ToString();

            if (numberStr.Contains("09376") || numberStr.Contains("90625"))
            {
                // F(x) 조건 체크
                run++;
                if (F(number) == 1)
                {
                    answer = temp;
                    UnityEngine.Debug.Log($"조건을 만족한 {index}번째 숫자는{answer}");
                }
            }
            else
                continue;

        }

        stopwatch.Stop();
        UnityEngine.Debug.Log($"소모시간 : {stopwatch.ElapsedMilliseconds}, F(x) 실행 횟수 : {run},\n{input}보다 작은 수에서 조건을 만족하는 가장 큰 수 : {answer}");
        stopwatch.Reset();
    }

    public int F(long x)
    {
        tempStr = temp.ToString();
        numberStr = numberStr.Remove(0, numberStr.Length - tempStr.Length);

        if (numberStr == tempStr)
        {
            return 1;
        }

        return 0;
    }
}
