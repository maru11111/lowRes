using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class golem : baseEnemy 
{

    Animator anim;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        hp = 100;
        power = 1;
        attackInterval = 3;

        if (isEnemy.Value)
        {

        }
        //眷属の場合
        else if (!isEnemy.Value)
        {
            //進行方向を反転
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

            //colliderのタグとレイヤーを変更
            gameObject.transform.Find("AttackCollider").gameObject.layer = LayerMask.NameToLayer("Friend");
            gameObject.transform.Find("AttackCollider").gameObject.tag = "Friend";
        }
        else
        {
            Debug.Log("isEnemyがnullです");
        }

            //anim = this.GetComponent<Animator>();
        //  anim.SetBool("Stand", true);
        /*
        if (anim.GetBool("Stand") == true)
        {
            Debug.Log("ああああああああああああああああ");
        }
        */
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    private void attackColliderOn()
    {
        //攻撃用の当たり判定をオンに(アニメーションから呼び出す用
        this.GetComponentInChildren<collider>().OnCollider();
    }
    private void attackColliderOff()
    {
        //攻撃用の当たり判定をオンに(アニメーションから呼び出す用
        this.GetComponentInChildren<collider>().OffCollider();
    }


    public override void attack(GameObject obj)
    {
        //自身が敵であれば
        if (isEnemy.Value)
        {


            //プレイヤー城にダメージ
            if (obj.tag == "PlayerCastle")
            {
                obj.GetComponent<castle>().damage(power);
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
            if (obj.tag == "EnemyCastle")
            {
                obj.GetComponent<castle>().damage(power);
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
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

    }
    
    private void finishAttack()
    {
        attacking = false;

        //dynamicに戻す
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
  
    }

    private void changeAttackMotion()
    {
        this.GetComponent<Animator>().SetBool("Attack", true);
        this.GetComponent<Animator>().SetBool("Stand", false);
        this.GetComponent<Animator>().SetBool("Walk", false);
    }
    private void changeWalkMotion()
    {
        this.GetComponent<Animator>().SetBool("Attack", false);
        this.GetComponent<Animator>().SetBool("Stand", false);
        this.GetComponent<Animator>().SetBool("Walk", true);
    }
    private void changeStandMotion()
    {
        this.GetComponent<Animator>().SetBool("Attack", false);
        this.GetComponent<Animator>().SetBool("Stand", true);
        this.GetComponent<Animator>().SetBool("Walk", false);
    }

    public override void move()
    {
        //眷属かどうかで変化しない

        //攻撃状態に移行していなければ歩く
        if (!moveAttack)
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
                if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack") && this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
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

     override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        //自身が敵であれば
        if (isEnemy.Value)
        {
            //当たったオブジェクトがプレイヤー城かつ攻撃状態に移行していなければ
            if (collision.gameObject.tag == "PlayerCastle" && !moveAttack)
            {
                //攻撃移行初期処理
                Debug.Log("プレイヤー城攻撃スタート");
                moveAttack = true;
                //はじめは即時攻撃
                timer = attackInterval;
                attacking = true;
            }
        }
        else if(!isEnemy.Value)
        {
            //当たったオブジェクトが敵城かつ攻撃状態に移行していなければ
            if (collision.gameObject.tag == "EnemyCastle" && !moveAttack)
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
