using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    [SerializeField]GameObject[] prefab;

    GameObject obj;
    public playMagicCircle magicCircle;

    private float interval=7;

    private float timer=0;

    private int i = 0;

    enum enemy
    {
        slimeGolem,
        debug
    }

    private IEnumerator delaySummonForMagicCircle()
    {
        //魔法陣が出るまで待つ
        yield return new WaitForSeconds(1.2f);

        obj = Instantiate(prefab[1], new Vector3(3.56f, 0, 0), Quaternion.identity);

        //isEnemyのために基底クラスを受け取る
        baseEnemy enemyScript = obj.GetComponent<baseEnemy>();

        if (enemyScript != null)
        {
            //敵か味方か決める
            if (i % 2 == 0)
            {
                enemyScript.isEnemy = true;
                Debug.Log("spawnIsEnemy");
            }
            else
            {
                enemyScript.isEnemy = false;
                Debug.Log("spawnIsEnemy");
            }
        }
        else
        {
            Debug.Log("BaseEnemyが見つかりませんでした");
        }
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
            //    obj = Instantiate(prefab[Random.Range(0,4)], new Vector3(3.56f, 0, 0), Quaternion.identity);

            if (i % 2 == 0)
            {
                //敵を生成
                magicCircle.MagicCirclePlay(new Vector3(3.56f, 0, 0));
                StartCoroutine(delaySummonForMagicCircle());

            }
            else
            {
                magicCircle.MagicCirclePlay(new Vector3(3.56f, 0, 0));
                StartCoroutine(delaySummonForMagicCircle());

            }
        
            i++;
            timer -= interval;
        }
        
    }
}
