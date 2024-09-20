using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class textCountDownTime : MonoBehaviour
{
    private float timer=0;
    private float deadWaitTime=11;
    public TextMeshProUGUI text;
    public bool isWaitEnd=false;

    public void resetTimer()
    {
        timer = 0;
        isWaitEnd = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        text.text = ((int)(deadWaitTime - timer)).ToString();

        if (deadWaitTime < timer)
        {
            isWaitEnd = true;
        }
    }
}
