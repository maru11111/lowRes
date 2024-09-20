using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class takeDamage : MonoBehaviour
{
    private Vector2 knockBackSpeed = new Vector2(10f, 3f);
    private Animator animator;
    public player playerScript;
    private float knockBackTime = 0.1f;
    private float stopTime = 1f;
    private float timer=0;
    private float timer2 = 0;
    public bool isKnockBack = false;
    public bool isStop = false;
    private int direction=0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.isDead)
        {
            if (isKnockBack)
            {
                knockBack();
            }
            if (isStop)
            {
                timer2 += Time.deltaTime;

                if (stopTime <= timer2)
                {
                    isStop = false;
                    timer2 = 0;
                    animator.SetBool("TakeDamage", false);
                }
            }
        }
    }

    public void startKnockBack(int dir)
    {
        if (!playerScript.isDead)
        {
            direction = dir;
            isKnockBack = true;
            isStop = true;
            animator.SetBool("TakeDamage", true);
            animator.SetBool("Dash", false);
        }
    }

    private void knockBack() 
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(knockBackSpeed.x*direction, knockBackSpeed.y);

        timer += Time.deltaTime;

        //ノックバック終了
        if(knockBackTime <= timer)
        {
            timer = 0;
            isKnockBack = false;
            GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        }
    }

}
