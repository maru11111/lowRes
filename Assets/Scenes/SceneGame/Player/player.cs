using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class player : MonoBehaviour
{

    public int maxHp=200;
    public int currentHp;
    private float timer=0;
    private float interval=1.5f;
    private bool muteki=false;


    // Start is called before the first frame update
    void Start()
    {
        currentHp=maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentHp = " + currentHp);
        if (currentHp <= 0)
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

    public void damage(int damage, Vector2 effectPos, GameObject otherObj)
    {
        if (!muteki)
        {
            currentHp -= damage;

            //吹っ飛ばし方向
            int direction;
            //プレイヤーの右側にぶつかれば
            if (otherObj.transform.position.x <= this.transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            //吹っ飛ばし
            GetComponent<takeDamage>().startKnockBack(direction);

            //衝突点にダメージエフェクト
            GameObject.Find("EffectScript").GetComponent<damageEffect>().damageEffectPlay(effectPos);
        
        //マイナスにならないようにする
        if (currentHp < 0)
            {
                currentHp = 0;
            }

            muteki = true;
        }
    }
}
