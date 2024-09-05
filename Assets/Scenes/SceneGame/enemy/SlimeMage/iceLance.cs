using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceLance : MonoBehaviour
{
    float timer=0;
    float attackTime=3;
    Vector3 dir;
    float speed = 2.5f;
    GameObject target;
    bool isEnemyAttack;
    int power=1;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            timer += Time.deltaTime;

            //発射前
            if (timer <= attackTime)
            {
                //槍からターゲットまでのベクトルを計算
                dir = target.transform.position - transform.position;
                //回転
                transform.rotation = Quaternion.FromToRotation(Vector3.down, dir);
            }
            //発射、発射後
            else
            {
                transform.position += speed * dir * Time.deltaTime;
            }
        }
    }
    
    public void setParam(GameObject obj, float t, bool isEnemy)
    {
        target = obj;
        attackTime = t;
        isEnemyAttack = isEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("col"+collision.tag);
        //敵なら
        if (isEnemyAttack)
        {
            //眷属にダメージ
            if (collision.CompareTag("Friend") || collision.CompareTag("FriendTank"))
            {
                var baseEnemyScript = collision.gameObject.GetComponent<baseEnemy>();
                if (baseEnemyScript != null)
                {
                    baseEnemyScript.damage(power, transform.position, this.gameObject);
                }
            }
            //プレイヤーにダメージ
            if (collision.CompareTag("Player"))
            {
                GameObject.Find("Player").GetComponent<player>().damage(power, transform.position, this.gameObject);
            }
            //プレイヤー城にダメージ
            if (collision.CompareTag("PlayerCastle"))
            {
                GameObject.Find("PlayerCastle").GetComponent<castle>().damage(power, transform.position);
            }
        }
        //眷属なら
        else
        {
            //敵にダメージ
            if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyTank"))
            {

                var baseEnemyScript = collision.gameObject.GetComponent<baseEnemy>();
                if (baseEnemyScript != null)
                {
                    baseEnemyScript.damage(power, transform.position, this.gameObject);
                }
            }
            //敵城にダメージ
            if (collision.CompareTag("EnemyCastle"))
            {
                GameObject.Find("EnemyCastle").GetComponent<castle>().damage(power, transform.position);
            }
        }
    }
}
