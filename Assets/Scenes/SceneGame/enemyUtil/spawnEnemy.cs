using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    [SerializeField]GameObject[] prefab;

    GameObject obj;

    private float interval=3;

    private float timer=0;

    private int i = 0;

    enum enemy
    {
        slimeGolem,
        debug
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用の敵生産
        timer += Time.deltaTime;
        if (interval < timer)
        {
            if (i % 2 == 0)
            {
                //敵を生成
                obj = Instantiate(prefab[0], new Vector3(3.56f, 0, 0), Quaternion.identity);
            }
            else
            {
                obj = Instantiate(prefab[0], new Vector3(3.56f, 0, 0), Quaternion.identity);
            }
            //isEnemyのために基底クラスを受け取る
            baseEnemy enemyScript = obj.GetComponent<baseEnemy>();

            if (enemyScript != null)
            {
                //敵か味方か決める
                if (i % 2 == 0)
                {
                    enemyScript.isEnemy = true;
                }
                else
                {
                    enemyScript.isEnemy = false;
                }
            }
            i++;
            timer -= interval;
        }
        
    }
}
