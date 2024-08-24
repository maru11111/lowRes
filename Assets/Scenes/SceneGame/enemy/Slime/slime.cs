using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class slime : baseEnemy
{
    private Animator anim;
    public GameObject playerCastle;
    public GameObject enemyCastle;
    public Rigidbody2D rigid;

    float attackStartWaitTime;

    float attackEndWaitTime;

    public bool flag = false;

    float speed = 1f;

    // Start is called before the first frame update
    override protected void Start()
    {
        maxHp = 50;
        power = 1;
        attackInterval = 1;
        rigid = GetComponent<Rigidbody2D>();

        if (isEnemy.Value)
        {
            playerCastle = GameObject.Find("PlayerCastle");
            target = playerCastle;
        }
        //眷属の場合
        else if (!isEnemy.Value)
        {
            //進行方向を反転
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

            enemyCastle = GameObject.Find("EnemyCastle");
            target = enemyCastle;
            //オブジェクトのタグとレイヤーを変更
            gameObject.layer = LayerMask.NameToLayer("Friend");
            gameObject.tag = "Friend";
            //colliderのタグとレイヤーを変更
            transform.Find("AttackCollider").gameObject.layer = LayerMask.NameToLayer("Friend");
            transform.Find("AttackCollider").gameObject.tag = "Friend";
            transform.Find("SearchCollider").gameObject.layer = LayerMask.NameToLayer("Friend");
            transform.Find("SearchCollider").gameObject.tag = "Friend";
        }
        else
        {
            Debug.Log("isEnemyがnullです");
        }

        base.Start();
    }
    private IEnumerator WaitForInitialization()
    {
        //isEnemyが初期化されるのを待つ
        while (isEnemy == null)
        {
            yield return null; //1フレーム待機
        }

    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        targetNullCheck();
       // startAttack(target);

        Debug.Log ("targetは" + target.name);
    }

    private void targetNullCheck()
    {
        if (target == null)
        {
            if (isEnemy.Value)
            {
                target = playerCastle;
            }
            else if(!isEnemy.Value)
            {
                target = enemyCastle;
            }
        }
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

    //ターゲットを決める(Stayで使用
    public void setAttackTarget(GameObject col)
    {
        //敵の場合
        if (isEnemy.Value)
        {
            //targetがnull(以前のターゲットが死亡後)または城の場合
            if (target == null || target.tag == "PlayerCastle")
            {
                //タンクかプレイヤーならターゲット
                if (col.tag == "FriendTank" || col.tag == "Player")
                {
                    target = col;
                }
            }
            //ターゲット変更
            else
            {
                //ターゲットがタンクでない(プレイヤーの)とき
                if (target.tag != "FriendTank")
                {
                    //タンクを優先してターゲット
                    if (col.tag == "FriendTank")
                    {
                        target = col;
                    }
                }
            }
        }
        //眷属の場合
        else
        {
            //targetがnull(以前のターゲットが死亡後)または城の場合
            if (target == null || target.tag == "EnemyCastle")
            {
                //タンクを優先してターゲット
                if(col.tag == "EnemyTank")
                {
                    target = col;
                }
                //付近の敵をターゲット
                else if (col.tag == "Enemy")
                {
                    target = col;
                }
            }
            //ターゲット変更
            else
            {
                //ターゲットがタンクでないとき
                if (target.tag != "EnemyTank")
                {
                    //タンクを優先してターゲット
                    if (col.tag == "EnemyTank")
                    {
                        target = col;
                    }
                }
            }
        }
    }

    //ターゲットを外す(Exitで使用
    public void removeAttackTarget(GameObject col)
    {
        //ターゲットがnullでないことが前提
        if (target != null)
        {
            //敵の場合
            if (isEnemy.Value)
            {
                if(col.tag == "Player")
                {
                    target = playerCastle;
                }
            }
            //眷属の場合
            else
            {
                //ターゲットした敵を追いかけ続ける
            }
        }
    }

    public void startAttack(GameObject target)
    {
        if (target != null)
        {
            rigid.AddForce(new Vector2(1000f, 0f));
        }
    }

    public override void attack(GameObject obj, Vector2 effectPos)
    {
         base.attack(obj, effectPos);
    }

    public override void move()
    {

        //進行方向を決める
        //ターゲットがnullでないことが前提
        if (target != null && flag==false)
        {
            //ターゲットが右にいたら
            if (transform.position.x <= target.transform.position.x)
            {
                //右を向く
                if (0 < this.transform.localScale.x)
                {
                    this.transform.localScale = new Vector3(-transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                }
                //右向きに進む
                rigid.velocity = new Vector2(speed, rigid.velocity.y);

            }
            else
            {
                //左を向く
                if (this.transform.localScale.x < 0)
                {
                    this.transform.localScale = new Vector3(-transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                }
                //左向きに進む
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
            }
        }
    }

    override protected void OnCollisionStay2D(Collision2D collision)
    {
        //プレイヤーが触れたらダメージ
        base.OnCollisionStay2D(collision);
    }
}
