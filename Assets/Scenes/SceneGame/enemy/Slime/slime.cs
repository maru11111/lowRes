using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class slime : baseEnemy
{
    public collider attackCol;

    // Start is called before the first frame update
    override protected void Start()
    {
        maxHp = 50;
        power = 1;
        walkInterval = 1f;
        attackCol = GetComponentInChildren<collider>();

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
            //colliderのタグとレイヤーを変更
            transform.Find("AttackCollider").gameObject.layer = LayerMask.NameToLayer("Friend");
            transform.Find("SearchCollider").gameObject.layer = LayerMask.NameToLayer("FriendSearchCollider");
            transform.Find("PhysicsCollider").gameObject.layer = LayerMask.NameToLayer("FriendPhysics");

        }
        else
        {
            Debug.Log("isEnemyがnullです");
        }

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        targetNullCheck();
    }

    //アニメーションで使用するメソッド
    private void attackColliderOn()
    {
        //攻撃用の当たり判定をオンに(アニメーションから呼び出す用
        attackCol.OnCollider();
    }
    private void attackColliderOff()
    {
        //攻撃用の当たり判定をオンに(アニメーションから呼び出す用
        attackCol.OffCollider();
    }
    
    private void changeWalkMotion()
    {
        anim.SetBool("Walk", true);
        anim.SetBool("Idle", false);
    }    
    private void changeIdleMotion()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Idle", true);
    }

    public void startAttack(GameObject target)
    {

    }

    public override void attack(GameObject obj, Vector2 effectPos)
    {
         base.attack(obj, effectPos);
    }

    public override void move()
    {
        //待機中
        if (anim.GetBool("Idle"))
        {
            timer += Time.deltaTime;
            
            if (walkInterval < timer)
            {
                setDirection();
                changeWalkMotion();
                timer -= walkInterval;
            }
        }
        //歩き(攻撃)中
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                changeIdleMotion();
            }
        }
    }

    override protected void OnTriggerStay2D(Collider2D collision)
    {
        //プレイヤーが触れたらプレイヤーにダメージ
        base.OnTriggerStay2D(collision);

        if (isEnemy.Value)
        {
            //城にぶつかったらdynamicに戻す
            if (collision.CompareTag("PlayerCastle"))
            {
                rigid.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        else
        {
            //城にぶつかったらdynamicに戻す
            if (collision.CompareTag("EnemyCastle"))
            {
                rigid.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
