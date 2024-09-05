using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mage : baseEnemy
{
    private float speed=1;
    public GameObject iceLancePrefab;

    // Start is called before the first frame update
    override protected void Start()
    {
        maxHp = 50;
        power = 1;
        attackInterval = 5;

        if (isEnemy.Value)
        {
            target = playerCastle;
            //進行方向を反転
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

        }
        //眷属の場合
        else if (!isEnemy.Value)
        {


            target = enemyCastle;
            //オブジェクトのタグとレイヤーを変更
            gameObject.layer = LayerMask.NameToLayer("Friend");
            gameObject.tag = "Friend";
            //colliderのレイヤーを変更
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
            //敵が射程圏内
            if (withinAttackRange)
            {
                //敵の方向を向く
                setDirection();
                //槍の作成開始
                StartCoroutine("startCreateLance");
                timer = 0;
            }
        }
    }
    private IEnumerator startCreateLance()
    {
        createLance(transform.position + new Vector3(-1f,0f,0f), 1f);

        yield return new WaitForSeconds(0.2f);

        createLance(transform.position + new Vector3(0f, 1f, 0f), 1f);

        yield return new WaitForSeconds(0.2f);

        createLance(transform.position + new Vector3(1f, 0f, 0f), 1f);
    }
    private void createLance(Vector3 pos, float time)
    {
        GameObject lance;
        
        lance = Instantiate(iceLancePrefab, pos, Quaternion.identity);
        lance.gameObject.GetComponent<iceLance>().setParam(target, time, isEnemy.Value);
        if (!isEnemy.Value)
        {
            lance.layer = LayerMask.NameToLayer("Friend");
        }
        Destroy(lance, 3f);
    }

    public override void move()
    {
        //上下にふわふわ
        transform.position += new Vector3(0, 0.2f*Time.deltaTime * Mathf.Sin(Time.time), 0);
        
        Debug.Log("within" + withinAttackRange);
        //敵が射程圏内なら
        if (withinAttackRange)
        {
            //左右に移動しない
        }
        //射程外
        else
        {
            //攻撃中でない
            if (!attacking/*!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")*/)
            {
                setDirection();
                walk();
            }
        }
    }
    private void walk()
    {
        //右を向いているとき
        if (0 <= transform.localScale.x)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);

        }
      
    }
}
