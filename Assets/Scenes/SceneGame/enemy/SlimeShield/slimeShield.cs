using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : baseEnemy
{
    public collider shieldCol;
    public collider attackCol;
    private guardEffect guardEffect;
    private bool isGuard=true;

    // Start is called before the first frame update
    override protected void Start()
    {
        maxHp = 50;
        power = 1;
        attackInterval = 2;
        walkInterval = 2;
        shieldCol = transform.Find("ShieldCollider").GetComponent<collider>();
        attackCol = transform.Find("AttackCollider1").GetComponent<collider>();

        guardEffect = GameObject.Find("EffectScript").GetComponent<guardEffect>();


        if (isEnemy.Value)
        {
            target = playerCastle;
        }
        //眷属の場合
        else if (!isEnemy.Value)
        {
            target = enemyCastle;
            //オブジェクトのタグとレイヤーを変更
            gameObject.layer = LayerMask.NameToLayer("Friend");
            gameObject.tag = "Friend";
            //colliderのレイヤーを変更
            transform.Find("AttackCollider1").gameObject.layer = LayerMask.NameToLayer("Friend");
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
    private void shieldColliderOn()
    {
        //ガード用の当たり判定をオンに(アニメーションから呼び出す用
        shieldCol.OnCollider();
    }
    private void shieldColliderOff()
    {
        //ガード用の当たり判定をオンに(アニメーションから呼び出す用
        shieldCol.OffCollider();
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
    private void isGuardOn()
    {
        isGuard = true;
    }
    private void isGuardOff()
    {
        isGuard = false;
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
            Debug.Log("onFloor: " + flagOnFloor);
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

    //盾ありダメージ処理
    public override void damage(int damage, Vector2 effectPos, GameObject other)
    {
        //ガード中
        if (isGuard)
        {
            //貫通攻撃の場合
            if (other.CompareTag("PiercingAttack"))
            {
                //Debug.Log("貫通攻撃！");
                //ダメージを受ける
                currentHp -= damage;
                //衝突点にダメージエフェクト
                damageEffect.damageEffectPlay(effectPos);
            }
            //貫通攻撃でない場合
            else
            {
                //Debug.Log("NOT貫通攻撃！ tag: "+other.tag +" obj: "+other);
                if (transform.position.x < other.transform.position.x && 0 < transform.localScale.x)
                {
                    //ガード
                    //衝突点にガードエフェクト
                    guardEffect.guardEffectPlay(effectPos);
                }
                else if (transform.position.x < other.transform.position.x && transform.localScale.x < 0)
                {
                    //ダメージを受ける
                    currentHp -= damage;
                    //衝突点にダメージエフェクト
                    damageEffect.damageEffectPlay(effectPos);
                }
                else if (other.transform.position.x < transform.position.x && transform.localScale.x < 0)
                {
                    //ガード
                    //衝突点にガードエフェクト
                    guardEffect.guardEffectPlay(effectPos);

                }
                else if (other.transform.position.x < transform.position.x && 0 < transform.localScale.x)
                {
                    //ダメージを受ける
                    currentHp -= damage;
                    //衝突点にダメージエフェクト
                    damageEffect.damageEffectPlay(effectPos);
                }
            }
        }
        //ガード中でない
        else
        {
            //ダメージを受ける
            currentHp -= damage;
            //衝突点にダメージエフェクト
            damageEffect.damageEffectPlay(effectPos);
        }
    }
}
