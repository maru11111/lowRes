using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class enemyD : baseEnemy
{

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        hp = 10;
        power = 3;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public override void attack(GameObject obj)
    {
        if (isEnemy.Value)
        {

        }
        else if (!isEnemy.Value)
        {

        }
    }

    public override void move()
    {

    }
}
