using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class baseEnemy : MonoBehaviour
{

    public int maxHp;
    public int currentHp;
    protected int power;
    protected int powerToCastle;
    protected float attackInterval;
    protected float walkInterval;
    protected float timer=0;
    protected float walkTimer;
    public bool? isEnemy=null;
    protected bool moveAttack=false;
    public bool withinAttackRange = false;
    protected bool attacking = false;
    public GameObject target;
    public bool flagOnFloor = false;
    public damageEffect damageEffect;
    protected castle playerCastleScript;
    protected castle enemyCastleScript;
    protected GameObject playerCastle;
    protected GameObject enemyCastle;
    public player player;
    public Rigidbody2D rigid;
    public Animator anim;

    public static int enemyNum=0;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        //敵と眷属の数カウント
        enemyNum += 1;
        Debug.Log("敵と眷属の数の合計 : " + enemyNum);

        damageEffect = GameObject.Find("EffectScript").GetComponent<damageEffect>();
       
        if (isEnemy.Value){
            playerCastleScript = GameObject.Find("PlayerCastle").GetComponent<castle>();
        }
        else
        {
            enemyCastleScript = GameObject.Find("EnemyCastle").GetComponent<castle>();
        }
        player = GameObject.Find("Player").GetComponent<player>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        currentHp = maxHp;

        //はじめは即時攻撃できるように
        timer = attackInterval;

        if (isEnemy.Value)
        {
            var canvas = transform.Find("Canvas");
            if (canvas != null)
            {
                //hpバーを非アクティブに
                canvas.gameObject.SetActive(false);
            }
            playerCastle = GameObject.Find("PlayerCastle");
        }
        //眷属の場合
        else if(!isEnemy.Value)
        {
            enemyCastle = GameObject.Find("EnemyCastle");
        }
        else
        {
            Debug.Log("isEnemyがnullです");
        }
    }

    private void OnDestroy()
    {
        enemyNum -= 1;
        Debug.Log("敵と眷属の数の合計 : " + enemyNum);

    }

    // Update is called once per frame
    virtual protected void Update()
    {
        defeat();
        move();
    }

    public void targetNullCheck()
    {
        if (target == null)
        {
            if (isEnemy.Value)
            {
                target = playerCastle;
            }
            else if (!isEnemy.Value)
            {
                target = enemyCastle;
            }
        }
    }

    //ダメージを受けるためのメソッド
    virtual public void damage(int damage, Vector2 effectPos)
    {
        currentHp -= damage;
        //衝突点にダメージエフェクト
        damageEffect.damageEffectPlay(effectPos);
    }
    //ダメージを受けるためのメソッド
    virtual public void damage(int damage, Vector2 effectPos, GameObject obj)
    {
        currentHp -= damage;
        //衝突点にダメージエフェクト
        damageEffect.damageEffectPlay(effectPos);
    }

    //死亡するためのメソッド
    public void destroy()
    {
        Destroy(this.gameObject);
    }

    //死亡処理のメソッド
    public void defeat()
    {
        if (currentHp <= 0)
        {
            destroy();
        }
    }

    //移動するためのメソッド
    virtual public void move()
    {
        if (isEnemy.Value)
        {

        }
    }

    //攻撃するためのメソッド
    virtual public void attack(GameObject obj, Vector2 effectPos)
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
                    baseEnemy.damage(power, effectPos);
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
                    baseEnemy.damage(power, effectPos);
                }
            }
        }
    }

    //ターゲットを決める(Stayで使用
    public void setAttackTarget(GameObject col)
    {
        //敵の場合
        if (isEnemy.Value)
        {
            //targetがnull(以前のターゲットが死亡後)または城の場合
            if (target == null || target.CompareTag("PlayerCastle"))
            {
                //タンクかプレイヤーならターゲット
                if (col.CompareTag("FriendTank") || col.CompareTag("Player"))
                {
                    target = col;
                }
                //付近の眷属をターゲット
                else if (col.CompareTag("Friend"))
                {
                    target = col;
                }
            }
            //ターゲット変更
            else
            {
                //ターゲットがタンクでない(プレイヤーの)とき
                if (!target.CompareTag("FriendTank"))
                {
                    //タンクを優先してターゲット
                    if (col.CompareTag("FriendTank"))
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
            if (target == null || target.CompareTag("EnemyCastle"))
            {
                //タンクを優先してターゲット
                if (col.CompareTag("EnemyTank"))
                {
                    target = col;
                }
                //付近の敵をターゲット
                else if (col.CompareTag("Enemy"))
                {
                    target = col;
                }
            }
            //ターゲット変更
            else
            {
                //ターゲットがタンクでないとき
                if (!target.CompareTag("EnemyTank"))
                {
                    //タンクを優先してターゲット
                    if (col.CompareTag("EnemyTank"))
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
                if (col.CompareTag("Player"))
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

    virtual protected void setDirection()
    {
        //進行方向を決める
        //ターゲットがnullでないことが前提
        if (target != null)
        {
            //ターゲットが右にいたら
            if (transform.position.x <= target.transform.position.x)
            {
                //右を向く
                if (this.transform.localScale.x < 0)
                {
                    this.transform.localScale = new Vector3(-transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                }
            }
            else
            {
                //左を向く
                if (0 < this.transform.localScale.x)
                {
                    this.transform.localScale = new Vector3(-transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                }
            }
        }
    }

    //アニメーション用
    //BodyTypeを変える
    private void changeKinematic()
    {
        rigid.bodyType = RigidbodyType2D.Kinematic;
    }
    private void changeDynamic()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
    }

    //自身に当たったオブジェクトのダメージ処理
    virtual protected void OnTriggerStay2D(Collider2D collision)
    {
        //自分が敵であれば
        if (isEnemy.Value)
        {
            //プレイヤーにダメージ
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 collisionPos = collision.ClosestPoint(this.transform.position);
                //ダメージ処理
                player.damage(power, collisionPos, this.gameObject);
            }
        }


        //床にいるか
        if (collision.gameObject.CompareTag("Stage"))
        {
            flagOnFloor = true;
        }
    }

    //床にいるか
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            flagOnFloor = false;
        }
    }
}