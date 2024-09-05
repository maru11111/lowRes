using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicCircle : MonoBehaviour
{
    public Animator anim;
    private float timer=0;
    private float diappearAnimationTime=2;

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (diappearAnimationTime < timer)
        {
            anim.SetBool("DisAppear",true);
        }
    }
}
