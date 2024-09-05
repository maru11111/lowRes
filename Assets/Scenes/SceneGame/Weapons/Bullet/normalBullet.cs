using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class normalBullet : MonoBehaviour
{

    enum Level
    {
        first,
        second,
        third
    }

    //(左なら-1右なら1上なら0, 上なら1それ以外は0)
    protected Vector2 direction = new (1,0);
    private float speed=10;
    private int power=10;
    private bool isShot=false;
    private float chargeTimer=0;
    private float chargeTime2nd=1.5f;
    private float chargeTime3rd=3.0f;
    private Level level = Level.first;
    private bool flagDestroyOnce=false;
    private bool isPenetrate;

    // Start is called before the first frame update
    void Start()
    {
        //打つ前は当たり判定オフに
        this.GetComponent<CircleCollider2D>().enabled = false;
        //弾の向きをプレイヤーが向いている方向に
        changeDir();
    }
    void Update()
    {
        //弾発射判定
        if (Input.GetKeyUp(KeyCode.S))
        {
            isShot = true;
        }
        //発射前
        if(!isShot)
        {
            //ダメージを受けたら攻撃解除
            if (GameObject.Find("Player").GetComponent<takeDamage>().isStop)
            {
                destroy();  
            }

            //レベル判定
            chargeTimer += Time.deltaTime;
            if (chargeTime3rd < chargeTimer)
            {
                level = Level.third;
            }
            else if(chargeTime2nd < chargeTimer)
            {
                level = Level.second;
            }

            //レベル別処理
            if (level == Level.first)
            {
                speed = 10f;
                power = 10;
                isPenetrate = false;
                gameObject.tag = "Untagged";
            }
            else if(level == Level.second)
            {
                power = 50;
                speed = 25f;
                isPenetrate = true;
                gameObject.tag = "PiercingAttack";
                this.transform.localScale = new(1.05f, 1.05f, 1);
            }
            else if(level == Level.third) 
            {
                power = 200;
                speed = 8f;
                isPenetrate = false;
                gameObject.tag = "Untagged";
                
                this.transform.localScale = new(1.3f, 1.3f, 1);
            }
            changeDir();
            move();
            //Debug.Log("Timer=" + chargeTimer);
        }
        //発射後
        else
        {
            if (!flagDestroyOnce)
            {
                Destroy(this.gameObject, 2);
                flagDestroyOnce = true;
                this.GetComponent<CircleCollider2D>().enabled = true;
            }
            shot();
        }
    }

    private void shot()
    {
        transform.position += new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
    }

    private void changeDir()
    {
        if (GameObject.Find("Player").GetComponent<move>().right)
        {
            direction = new(1, 0);
        }
        else
        {
            direction = new(-1, 0);
        }
        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    right = true;
        //    this.GetComponent<SpriteRenderer>().flipX = true;
        //}
        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{
        //上ボタンを押しかつ左右どちらにも動いていなければ
        if (GameObject.Find("Player").GetComponent<move>().up && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            direction = new(0, 1);
        }
    }

    private void move()
    {
        //右
        if (GameObject.Find("Player").GetComponent<move>().right) {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(1, 0, 0) ;
        }
        //左
        else if(!GameObject.Find("Player").GetComponent<move>().right)
        {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(-1, 0, 0);
        }
        //上
        if (GameObject.Find("Player").GetComponent<move>().up && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 1, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var baseEnemy = collision.gameObject.GetComponent<baseEnemy>();

        if (baseEnemy != null)
        {
            //敵にダメージ
            if (collision.gameObject.tag == "Enemy"||collision.gameObject.tag=="EnemyTank")
            {
                //衝突したオブジェクトの位置を取得
                Vector2 collisionPos = collision.ClosestPoint(transform.position);
                
                baseEnemy.damage(power, collisionPos, this.gameObject);
                
                if (level == Level.first || level == Level.second)
                {
                    hitStop.hitStopInstance.startHitStop(0.03125f);
                }
                else if(level == Level.third)
                {
                    hitStop.hitStopInstance.startHitStop(0.0625f);
                }

                //自身を削除
                destroy();
            }
        }

        var castle = collision.gameObject.GetComponent<castle>();

        if(castle != null)
        {
            //城にダメージ
            if(collision.gameObject.tag == "EnemyCastle")
            {
                //衝突したオブジェクトの位置を取得
                Vector2 collisionPos = collision.ClosestPoint(transform.position);

                castle.damage(power, collisionPos);
            }

            //自身を削除
            destroy();
        }

    }

    public void destroy()
    {
        //貫通弾でなければ
        if (!isPenetrate)
        {
            Destroy(this.gameObject);
        }
    }

}
