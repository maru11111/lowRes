using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class golem : baseEnemy 
{

    public collider attackCol;

    // Start is called before the first frame update
    override protected void Start()
    {
        maxHp = 100;
        power = 1;
        attackInterval = 3;

        attackCol = GetComponentInChildren<collider>();

        if (isEnemy.Value)
        {
            //オブジェクトのタグを変更
            gameObject.tag = "EnemyTank";
        }
        //眷属の場合
        else if (!isEnemy.Value)
        {
            //進行方向を反転
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

            //オブジェクトのタグとレイヤーを変更
            gameObject.layer = LayerMask.NameToLayer("Friend");
            gameObject.tag = "FriendTank";
            //colliderのタグとレイヤーを変更
            transform.Find("AttackCollider").gameObject.layer = LayerMask.NameToLayer("Friend");
            transform.Find("PhysicsCollider").gameObject.layer = LayerMask.NameToLayer("TankFriendPhysics");
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
    }

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
    public override void attack(GameObject obj, Vector2 effectPos)
    {
        //自身が敵であれば
        if (isEnemy.Value)
        {
            //プレイヤー城にダメージ
            if (obj.CompareTag("PlayerCastle"))
            {
                playerCastleScript.damage(power, effectPos);
            }
            else
            {
                Debug.Log("");
            }
        }
        //自身が眷属であれば
        else if(!isEnemy.Value)
        {
            //敵城にダメージ
            if (obj.CompareTag("EnemyCastle"))
            {
                enemyCastleScript.damage(power, effectPos);
            }
            else
            {
                Debug.Log("");
            }
        }
    }

    private IEnumerator setAttack()
    {
        Debug.Log("攻撃モーション移行");
        attacking = true;

        //壁の前で止まる(これがないと攻撃時に埋まる
        changeStandMotion();
        yield return new WaitForFixedUpdate();

        //攻撃モーションに変更
        changeAttackMotion();

        //kinematicにしてアニメーションで位置を操作できるようにする
        rigid.bodyType = RigidbodyType2D.Kinematic;

    }
    
    private void finishAttack()
    {
        attacking = false;

        //dynamicに戻す
        rigid.bodyType = RigidbodyType2D.Dynamic;
  
    }

    private void changeAttackMotion()
    {
        anim.SetBool("Attack", true);
        anim.SetBool("Stand", false);
        anim.SetBool("Walk", false);
    }
    private void changeWalkMotion()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Stand", false);
        anim.SetBool("Walk", true);
    }
    private void changeStandMotion()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Stand", true);
        anim.SetBool("Walk", false);
    }

    public override void move()
    {
        //眷属かどうかで変化しない

        //攻撃状態に移行していないかつ床にいれば歩く
        if (!moveAttack && flagOnFloor)
        {
            changeWalkMotion();
        }
        //攻撃可能状態に移行していれば(城に着いていれば)
        else if (moveAttack)
        {
            timer += Time.deltaTime;

            //攻撃中であれば(待機時間でなければ)
            if (attacking)
            {

                //攻撃が終了していれば
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    //攻撃終了処理
                    finishAttack();
                    Debug.Log("攻撃終了");
                }
            }
            else
            {
                Debug.Log("待機");
                changeStandMotion();
            }

            //攻撃待機時間が終わっていれば
            if (attackInterval < timer)
            {
                StartCoroutine(setAttack());

                //タイマーリセット
                timer -= attackInterval;
            }
        }
    }

     override protected void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        //攻撃処理
        //自身が敵であれば
        if (isEnemy.Value)
        {
            //当たったオブジェクトがプレイヤー城かつ攻撃状態に移行していなければ
            if (collision.gameObject.CompareTag("PlayerCastle") && !moveAttack)
            {
                //攻撃移行初期処理
                Debug.Log("プレイヤー城攻撃スタート");
                moveAttack = true;
                //はじめは即時攻撃
                timer = attackInterval;
                attacking = true;
            }
        }
        //眷属であれば
        else if(!isEnemy.Value)
        {
            //当たったオブジェクトが敵城かつ攻撃状態に移行していなければ
            if (collision.gameObject.CompareTag("EnemyCastle") && !moveAttack)
            {
                //攻撃移行初期処理
                Debug.Log("敵城攻撃スタート");
                moveAttack = true;
                //はじめは即時攻撃
                timer = attackInterval;
                attacking = true;
            }
        }

    }

}
