using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mage : baseEnemy
{
    private float speed = 1;
    public GameObject iceLancePrefab;
    public List<GameObject> iceLances;
    public int rancePower;

    // Start is called before the first frame update
    override protected void Start()
    {
        if (isEnemy.Value)
        {
            maxHp = 16;
            power = 1;
            rancePower = 20;
        }
        else
        {
            maxHp = 21;
            power = 1;
            rancePower = 15;

            //スキル補正
            switch (SaveDataManager.data.friendStrengthenLevel)
            {
                case 0:
                    //変化なし
                    break;

                case 1:
                    maxHp += 20;
                    rancePower += 5;
                    break;

                case 2:
                    maxHp += 30;
                    rancePower += 10;
                    break;

                case 3:
                    maxHp += 40;
                    rancePower += 15;
                    break;
            }
        }

        attackInterval = 5;
        friendType = FriendType.mage;

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
        createLance(transform.position + new Vector3(-1f, 0f, 0f), 1f);

        yield return new WaitForSeconds(0.2f);

        createLance(transform.position + new Vector3(0f, 1f, 0f), 1f);

        yield return new WaitForSeconds(0.2f);

        createLance(transform.position + new Vector3(1f, 0f, 0f), 1f);
    }
    private void createLance(Vector3 pos, float time)
    {
        GameObject lance;

        lance = Instantiate(iceLancePrefab, pos, Quaternion.identity);
        iceLances.Add(lance);
        lance.gameObject.GetComponent<iceLance>().setParam(target, time, isEnemy.Value, rancePower);
        if (!isEnemy.Value)
        {
            lance.layer = LayerMask.NameToLayer("Friend");
        }
        Destroy(lance, 3f);
    }

    public override void move()
    {
        //上下にふわふわ
        transform.position += new Vector3(0, 0.2f * Time.deltaTime * Mathf.Sin(Time.time), 0);

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

    //死んだとき
    public override void destroy()
    {
        //槍があったら消す
        for(int i = 0; i < iceLances.Count; i++) {
            Destroy(iceLances[i]);
        }

        base.destroy();
    }
}
