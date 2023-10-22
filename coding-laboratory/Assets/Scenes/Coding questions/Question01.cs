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
            UnityEngine.Debug.Log("���ڿ� �ν��� �����մϴ�.");
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
                // F(x) ���� üũ
                run++;
                if (F(number) == 1)
                {
                    answer = temp;
                    UnityEngine.Debug.Log($"������ ������ {index}��° ���ڴ�{answer}");
                }
            }
            else
                continue;

        }

        stopwatch.Stop();
        UnityEngine.Debug.Log($"�Ҹ�ð� : {stopwatch.ElapsedMilliseconds}, F(x) ���� Ƚ�� : {run},\n{input}���� ���� ������ ������ �����ϴ� ���� ū �� : {answer}");
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
