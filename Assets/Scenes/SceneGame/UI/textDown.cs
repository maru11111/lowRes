using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class textDown : MonoBehaviour
{
    private float timer=0;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer < 1f)
        {
            text.text = "ダウン中.";
        }
        else if(timer < 2f)
        {
            text.text = "ダウン中..";
        }
        else if(timer < 3f)
        {
            text.text = "ダウン中...";
        }
        else if(timer < 4f)
        {
            //text.text = "ダウン中...";
            timer = 0f;
        }
        else if(timer < 5f)
        {

        }
    }
}
