using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class player : MonoBehaviour
{

    public int hp=20;
    private float timer=0;
    private float interval=3;
    private bool muteki=false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }


        if (muteki)
        {
            timer += Time.deltaTime;
            if (interval < timer)
            {
                muteki = false;
                timer = 0;
            }
        }
    }

    public void damage(int damage)
    {
        if (!muteki)
        {
            hp -= damage;

            muteki = true;
        }



    }
}
