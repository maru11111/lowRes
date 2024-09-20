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
    private int power=0;
    private int powerLevel1 = 10;
    private int powerLevel2 = 8;
    private int powerLevel3 = 200;
    private bool isShot=false;
    private float chargeTimer=0;
    private float chargeTime2nd=1.5f;
    private float chargeTime3rd=3.0f;
    private Level level = Level.first;
    private bool flagDestroyOnce=false;
    private bool isPenetrate;
    public static int bulletInstanceNum=0;
    public static int bulletInstanceMaxNum=3;

    public Animator anim;

    public SpriteRenderer spriteRenderer;
    public Sprite[] images;

    // Start is called before the first frame update
    void Start()
    {
        
        //プレイヤー攻撃力レベル補正
        if(SaveDataManager.data.playerPowerLevel == 0)
        {
            //変化なし
        }
        else if(SaveDataManager.data.playerPowerLevel == 1)
        {
            powerLevel1 += 2;
            powerLevel2 += 8;
            powerLevel3 += 50;
        }
        else if(SaveDataManager.data.playerPowerLevel == 2)
        {
            powerLevel1 += 3;
            powerLevel2 += 16;
            powerLevel3 += 100;
        }
        else if(SaveDataManager.data.playerPowerLevel == 3)
        {
            powerLevel1 += 6;
            powerLevel2 += 24;
            powerLevel3 += 150;
        }

        //打つ前は当たり判定オフに
        this.GetComponent<CircleCollider2D>().enabled = false;
        //弾の向きをプレイヤーが向いている方向に
        changeDir();
        //弾のインスタンス数を増やす
        bulletInstanceNum++;
    }

    private void OnDestroy()
    {
        //弾のインスタンス数を減らす
        bulletInstanceNum--;
    }
    void Update()
    {
        //弾発射判定
        if (Input.GetKeyUp(KeyCode.S))
        {
            isShot = true;
        }
        //発射前
        if (!isShot)
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
            else if (chargeTime2nd < chargeTimer)
            {
                level = Level.second;
            }

            //レベル別処理
            switch (level)
            {
                case Level.first:
                    speed = 10f;
                    power = powerLevel1;
                    isPenetrate = false;
                    gameObject.tag = "Untagged";
                    spriteRenderer.sprite = images[0];
                  
                    break;

                case Level.second:
                    power = powerLevel2;
                    speed = 25f;
                    isPenetrate = true;
                    gameObject.tag = "PiercingAttack";
                    this.transform.localScale = new(1.05f, 1.05f, 1);
                    spriteRenderer.sprite = images[1];
                   
                    break;

                case Level.third:
                    power = powerLevel3;
                    speed = 8f;
                    isPenetrate = false;
                    gameObject.tag = "Untagged";
                    this.transform.localScale = new(1.3f, 1.3f, 1);
                    spriteRenderer.sprite = images[2];
                  
                    break;
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
                switch (level)
                {
                    case Level.first:
                        Destroy(this.gameObject, 0.375f);
                        break;

                    case Level.second:
                        Destroy(this.gameObject, 2);
                        break;
                    
                    case Level.third:
                        Destroy(this.gameObject, 2);
                        break;
                }
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
            switch (level)
            {
                case Level.first:
                    transform.Rotate(0, 0, -90 * Time.deltaTime);
                    break;
                case Level.second:
                    transform.Rotate(0, 0, -480*2 * Time.deltaTime);
                    break;
                case Level.third:
                    transform.Rotate(0, 0, -15 * Time.deltaTime);
                    break;
            }
        }
        else
        {
            direction = new(-1, 0);
            transform.Rotate(0, 0, 15 * Time.deltaTime);
            switch (level)
            {
                case Level.first:
                    transform.Rotate(0, 0, 90 * Time.deltaTime);
                    break;
                case Level.second:
                    transform.Rotate(0, 0, 480*2 * Time.deltaTime);
                    break;
                case Level.third:
                    transform.Rotate(0, 0, 15 * Time.deltaTime);
                    break;
            }
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
                //自身を削除
                destroy();
            }
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
