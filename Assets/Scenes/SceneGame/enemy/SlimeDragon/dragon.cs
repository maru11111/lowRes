using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : baseEnemy
{
    public collider attackCol1;
    public collider attackCol2;

    // Start is called before the first frame update
    override protected void Start()
    {
        maxHp = 50;
        power = 1;
        attackInterval = 2;
        walkInterval = 1;
        attackCol1 = transform.Find("AttackCollider1").GetComponent<collider>();
        attackCol2 = transform.Find("AttackCollider2").GetComponent<collider>();


        //baseに移動

        ////はじめは即時攻撃できるように
        //timer = attackInterval;
        
        if (isEnemy.Value)
        {
            target = playerCastle;
        }
        //眷属の場合
        else if (!isEnemy.Value)
        {
            //進行方向を反転
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

            target = enemyCastle;
            //オブジェクトのタグとレイヤーを変更
            gameObject.layer = LayerMask.NameToLayer("Friend");
            gameObject.tag = "Friend";
            //colliderのレイヤーを変更
            transform.Find("AttackCollider1").gameObject.layer = LayerMask.NameToLayer("Friend");
            transform.Find("AttackCollider2").gameObject.layer = LayerMask.NameToLayer("Friend");
            transform.Find("SearchCollider").gameObject.layer = LayerMask.NameToLayer("FriendSearchCollider");
            transform.Find("SearchColliderForAttack").gameObject.layer = LayerMask.NameToLayer("FriendSearchCollider");
            transform.Find("PhysicsCollider").gameObject.layer = LayerMask.NameToLayer("FriendPhysics");
        }
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        targetNullCheck();

        //Debug.Log("walkTimer"+walkTimer);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            timer += Time.deltaTime;
        }

        if (attackInterval <= timer)
        {
            //敵が射程圏内かつ歩き中でない
            if (withinAttackRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                //敵の方向を向く
                setDirection();
                //攻撃に移る
                anim.SetTrigger("Attack");
                timer = 0;
            }
        }
    }

    //アニメーションで使用するメソッド
    private void attackColliderOn()
    {
        //攻撃用の当たり判定をオンに(アニメーションから呼び出す用
        attackCol1.OnCollider();
        attackCol2.OnCollider();
    }
    private void attackColliderOff()
    {
        //攻撃用の当たり判定をオンに(アニメーションから呼び出す用
        attackCol1.OffCollider();
        attackCol2.OffCollider();
    }

    public override void move()
    {
        //敵が射程圏内なら
        if (withinAttackRange)
        {
            //移動しない
        }
        //射程外
        else
        {
            walkTimer += Time.deltaTime;

            //床にいる
            if (flagOnFloor)
            {
                if (walkInterval <= walkTimer)
                {
                    //攻撃中でない
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        setDirection();
                        anim.SetTrigger("Walk");
                        walkTimer = 0;
                    }
                }
            }
        }
    }
}
