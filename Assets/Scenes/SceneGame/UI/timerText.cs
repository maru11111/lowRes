using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timerText : MonoBehaviour
{
    //タイムリミット(秒)
    private float timeLimit = 181f;
    public float currentRemainingTime=0;
    public float timer=0;
    public bool isTimeLimit=false;

    public TextMeshProUGUI text;
    private int minuteNum = 0;
    private int secondNum = 0;

    public float getTimeLimit()
    {
        return timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimeLimit)
        {
            timer += Time.deltaTime;

            if (timeLimit < timer)
            {
                isTimeLimit = true;
            }
        }

        //のこり時間を分と秒に分解
        currentRemainingTime = timeLimit - timer;

        minuteNum = (int)(currentRemainingTime / 60);
        secondNum = (int)(currentRemainingTime - minuteNum * 60);

        //テキスト表示
        if (secondNum < 10)
        {
            text.text = "0" + minuteNum.ToString() + ":" + "0" + secondNum.ToString();
        }
        else
        {
            text.text = "0" + minuteNum.ToString() + ":" + secondNum.ToString();
        }
    }
}
