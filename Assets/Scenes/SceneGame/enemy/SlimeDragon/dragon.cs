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
        if (isEnemy.Value)
        {
            maxHp = 120;
            power = 50;
        }
        else
        {
            maxHp = 180;
            power = 75;

            //スキル補正
            switch (SaveDataManager.data.friendStrengthenLevel)
            {
                case 0:
                    //変化なし
                    break;

                case 1:
                    maxHp += 20;
                    power += 10;
                    break;

                case 2:
                    maxHp += 60;
                    power += 20;
                    break;

                case 3:
                    maxHp += 100;
                    power += 30;
                    break;
            }
        }
        attackInterval = 4;
        walkInterval = 1;
        friendType = FriendType.dragon;
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
            transform.Find("AttackCollider1").gameObject.tag = ("PiercingAttack");
            transform.Find("AttackCollider2").gameObject.tag = ("PiercingAttack");
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
            //敵が射程圏内かつ歩き中でないかつ床にいる
            if (withinAttackRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") && flagOnFloor)
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
            //プレイヤーにダメージ
            if (obj.CompareTag("Player"))
            {
                player.damage(power, effectPos, this.gameObject);
            }
            //眷属にダメージ
            if (obj.CompareTag("Friend") || obj.CompareTag("FriendTank"))
            {
                var baseEnemy = obj.GetComponent<baseEnemy>();
                if (baseEnemy != null)
                {
                    Debug.Log(obj + "に" + power + "damage");
                    baseEnemy.damage(power, effectPos, attackCol1.gameObject);
                }
            }
        }
        //自身が眷属であれば
        else if (!isEnemy.Value)
        {
            //敵城にダメージ
            if (obj.CompareTag("EnemyCastle"))
            {
                enemyCastleScript.damage(power, effectPos);
            }
            //敵にダメージ
            if (obj.CompareTag("Enemy") || obj.CompareTag("EnemyTank"))
            {
                var baseEnemy = obj.GetComponent<baseEnemy>();
                if (baseEnemy != null)
                {
                    baseEnemy.damage(power, effectPos, attackCol1.gameObject);
                }
            }
        }
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

    override public void destroy()
    {
        //自身が敵だった場合
        if (isEnemy.Value)
        {
            //レベル1では確定ドロップ
            if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level1) 
            {
                //眷属アイテム作成
                GameObject item = Instantiate(friendItemPrefab, transform.position, Quaternion.identity);
                item.GetComponent<friendItem>().setParam(friendType);
            }
            else
            {
                //一定確率で眷属になる
                int ProbabilityNum = Random.Range(0, 100);
                Debug.Log(player.getProbability() + "以下であれば眷属 num : " + ProbabilityNum);
                //probability(％)の確率で
                if (ProbabilityNum < player.getProbability())
                {
                    //眷属アイテム作成
                    GameObject item = Instantiate(friendItemPrefab, transform.position, Quaternion.identity);
                    item.GetComponent<friendItem>().setParam(friendType);
                }
                //はずれ
                else
                {
                    //眷属にならない
                }
            }

        }
        Destroy(this.gameObject);
    }
}
