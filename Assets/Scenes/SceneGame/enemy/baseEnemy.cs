using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class baseEnemy : MonoBehaviour
{

    protected int hp;
    protected int power;
    protected int powerToCastle;
    protected float attackInterval;
    protected float timer=0;
    public bool? isEnemy=null;
    protected bool moveAttack=false;
    protected bool attacking = false;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (isEnemy.Value)
        {

        }
        //眷属の場合
        else if(!isEnemy.Value)
        {
            //タグとレイヤーを変更
            this.gameObject.layer = LayerMask.NameToLayer("Friend");
            this.gameObject.tag = "Friend";
        }
        else
        {
            Debug.Log("isEnemyがnullです");
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        defeat();
        move();
        //attack();
    }

    //ダメージを受けるためのメソッド
    public void damage(int damage)
    {
        hp -= damage;
    }

    //死亡するためのメソッド
    public void destroy()
    {
        Destroy(this.gameObject);
    }

    //死亡処理のメソッド
    public void defeat()
    {
        if (hp <= 0)
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
    virtual public void attack(GameObject obj)
    {
        if (!isEnemy.Value)
        {

        }
    }

    //自身に当たった弾の削除処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnemy.Value)
        {
            //弾に当たれば弾のdestroy処理
            if (collision.gameObject.tag == "Bullet")
            {
                collision.GetComponent<normalBullet>().destroy();
            }
        }
    }

    //自身に当たったオブジェクトのダメージ処理
    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        //自分が敵であれば
        if (isEnemy.Value)
        {
            //プレイヤーにダメージ
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<player>().damage(power);
            }

            //眷属にダメージ
            if (collision.gameObject.tag == "Friend")
            {
                collision.gameObject.GetComponent<baseEnemy>().damage(power);
            }
        }
        //自分が眷属であれば
        else if(!isEnemy.Value)
        {
            //敵にダメージ
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<baseEnemy>().damage(power);
            }
        }
    }
}
