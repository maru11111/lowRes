using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    [SerializeField]GameObject[] prefab;
    GameObject obj;
    public playMagicCircle magicCircle;
    public castle enemyCastle;

    struct Timer
    {
        public float timer;
        public float interval;
    }

    private Timer normalTimer;
    private Timer dragonTimer;
    private Timer mageTimer;

    private EnemyKind kind;
    private float spawnPosX=0;
    private float spawnPosYslime = -0.5f;
    private float spawnPosYshield = -0.5f;
    private float spawnPosYgolem = -0.5f;
    private float spawnPosYmage = -1.95f;
    private float spawnPosYdragon = -0.5f;

    private int spawnPosIdx;

    public GameObject[] spawnPoint;

    private int probabilityNum;
    private bool halfHpEvent = false;

    public enum EnemyKind
    {
        slime,
        shield,
        golem,
        mage,
        dragon,
        terminal
    }

    private void Start()
    {
        switch (CommonParam.selectStageLevel)
        {
            case CommonParam.StageLevel.Level1:
                normalTimer.timer = 0;
                normalTimer.interval = 7f;
                break;
            case CommonParam.StageLevel.Level2:
                normalTimer.timer = 0;
                normalTimer.interval = 5.0f;
                dragonTimer.timer = 0;
                dragonTimer.interval = 25f;
                mageTimer.timer = 0;
                mageTimer.interval = 30;
                break;
            case CommonParam.StageLevel.Level3:
                normalTimer.timer = 0;
                normalTimer.interval = 4.5f;
                dragonTimer.timer = 0;
                dragonTimer.interval = 25f;
                mageTimer.timer = 0;
                mageTimer.interval = 35;
                break;
        }

        normalTimer.timer = normalTimer.interval;

        //デバッグ
        if(CommonParam.selectStageLevel == CommonParam.StageLevel.NotSelected)
        {
            CommonParam.selectStageLevel = CommonParam.StageLevel.Level1;
        }
    }

    private IEnumerator delaySummonForMagicCircle(EnemyKind kind, float posX)
    {
        //魔法陣生成
        switch (kind)
        {
            case EnemyKind.slime:  magicCircle.MagicCirclePlay(new Vector3(posX, spawnPosYslime, 0)); break;
            case EnemyKind.shield: magicCircle.MagicCirclePlay(new Vector3(posX, spawnPosYshield, 0)); break;
            case EnemyKind.golem:  magicCircle.MagicCirclePlay(new Vector3(posX, spawnPosYgolem, 0)); break;
            case EnemyKind.mage:   magicCircle.MagicCirclePlay(new Vector3(posX, spawnPosYmage, 0)); break;
            case EnemyKind.dragon: magicCircle.MagicCirclePlay(new Vector3(posX, spawnPosYdragon, 0)); break;
        }

        //魔法陣が出るまで待つ
        yield return new WaitForSeconds(1.2f);

        //インスタンス生成
        switch (kind)
        {
            case EnemyKind.slime:  obj = Instantiate(prefab[(int)EnemyKind.slime],  new Vector3(posX, spawnPosYslime, 0), Quaternion.identity); break;
            case EnemyKind.shield: obj = Instantiate(prefab[(int)EnemyKind.shield], new Vector3(posX, spawnPosYshield, 0), Quaternion.identity); break;
            case EnemyKind.golem:  obj = Instantiate(prefab[(int)EnemyKind.golem],  new Vector3(posX, spawnPosYgolem, 0), Quaternion.identity); break;
            case EnemyKind.mage:   obj = Instantiate(prefab[(int)EnemyKind.mage],   new Vector3(posX, spawnPosYmage, 0), Quaternion.identity); break;
            case EnemyKind.dragon: obj = Instantiate(prefab[(int)EnemyKind.dragon], new Vector3(posX, spawnPosYdragon, 0), Quaternion.identity); break;
        }


        //isEnemyのために基底クラスを受け取る
        baseEnemy enemyScript = obj.GetComponent<baseEnemy>();

        if (enemyScript != null)
        {
            enemyScript.isEnemy = true;
        }
        else
        {
            Debug.Log("BaseEnemyが見つかりませんでした");
        }
    }

    // Update is called once per frame
    void Update()
    {
        normalTimer.timer += Time.deltaTime;

        switch (CommonParam.selectStageLevel)
        {
            case CommonParam.StageLevel.Level1:
                if (normalTimer.interval <= normalTimer.timer)
                {

                    //敵城のHPが半分以上なら
                    if (enemyCastle.maxHp / 2 <= enemyCastle.currentHp)
                    {
                        //出現する敵を決める
                        setKindForProbability(70, 10, 20, 0, 0);
                    }
                    //敵城のHPが半分より少ないなら
                    else
                    {
                        //HP半分イベント
                        if (!halfHpEvent)
                        {
                            StartCoroutine(level1HalfHpEvent());
                            halfHpEvent = true;
                        }

                        //出現する敵を決める
                        setKindForProbability(60, 20, 20, 0, 0);
                    }

                    //出現する座標を決める
                    spawnPosIdx = Random.Range(0, spawnPoint.Length);
                    spawnPosX = spawnPoint[spawnPosIdx].transform.position.x;
                    //敵を出す
                    StartCoroutine(delaySummonForMagicCircle(kind, spawnPosX));
                    normalTimer.timer -= normalTimer.interval;
                }

                break;

            case CommonParam.StageLevel.Level2:

                if (normalTimer.interval <= normalTimer.timer)
                {
                    //敵城のHPが半分以上なら
                    if (enemyCastle.maxHp / 2 <= enemyCastle.currentHp)
                    {
                        //出現する敵を決める
                        setKindForProbability(50, 30, 20, 0, 0);
                    }
                    //敵城のHPが半分より少ないなら
                    else
                    {
                        //HP半分イベント
                        if (!halfHpEvent)
                        {
                            StartCoroutine(level2HalfHpEvent());
                            halfHpEvent = true;
                        }

                        //出現する敵を決める
                        setKindForProbability(45, 35, 20, 0, 0);
                    }

                    //出現する座標を決める
                    spawnPosIdx = Random.Range(0, spawnPoint.Length);
                    spawnPosX = spawnPoint[spawnPosIdx].transform.position.x;
                    //敵を出す
                    StartCoroutine(delaySummonForMagicCircle(kind, spawnPosX));
                    normalTimer.timer -= normalTimer.interval;
                }

                //敵城のHPが半分以上なら
                if (enemyCastle.maxHp / 2 <= enemyCastle.currentHp)
                {
                    //ドラゴンスライムが一定間隔で登場

                    dragonTimer.timer += Time.deltaTime;

                    if(dragonTimer.interval <= dragonTimer.timer)
                    {
                        //出現する座標を決める
                        spawnPosIdx = Random.Range(0, spawnPoint.Length);
                        spawnPosX = spawnPoint[spawnPosIdx].transform.position.x;
                        //敵を出す
                        StartCoroutine(delaySummonForMagicCircle(EnemyKind.dragon, spawnPosX));
                        dragonTimer.timer -= dragonTimer.interval;
                    }
                }
                //半分より少ないなら
                else
                {
                    //メイジスライムが一定間隔で出現

                    mageTimer.timer -= Time.deltaTime;

                    if(mageTimer.interval <= mageTimer.timer)
                    {
                        //出現する座標を決める
                        spawnPosIdx = Random.Range(0, spawnPoint.Length);
                        spawnPosX = spawnPoint[spawnPosIdx].transform.position.x;
                        //敵を出す
                        StartCoroutine(delaySummonForMagicCircle(EnemyKind.mage, spawnPosX));
                        mageTimer.timer -= mageTimer.interval;
                    }
                }
        
                break;

            case CommonParam.StageLevel.Level3:
                if (normalTimer.interval < normalTimer.timer)
                {
                    //敵城のHPが半分以上なら
                    if (enemyCastle.maxHp / 2 <= enemyCastle.currentHp)
                    {
                        //出現する敵を決める
                        setKindForProbability(30, 40, 24, 3, 3);
                    }
                    //敵城のHPが半分より少ないなら
                    else
                    {
                        //HP半分イベント
                        if (!halfHpEvent)
                        {
                            StartCoroutine(level3HalfHpEvent());
                            halfHpEvent = true;
                        }

                        //出現する敵を決める
                        setKindForProbability(27, 37, 24, 6, 6);
                    }

                    //出現する座標を決める
                    spawnPosIdx = Random.Range(0, spawnPoint.Length);
                    spawnPosX = spawnPoint[spawnPosIdx].transform.position.x;
                    //敵を出す
                    StartCoroutine(delaySummonForMagicCircle(kind, spawnPosX));
                    normalTimer.timer -= normalTimer.interval;
                }

                //敵城のHPが半分以上なら
                if (enemyCastle.maxHp / 2 <= enemyCastle.currentHp)
                {
                    //ドラゴンスライムが一定間隔で登場

                    dragonTimer.timer += Time.deltaTime;

                    if (dragonTimer.interval <= dragonTimer.timer)
                    {
                        //出現する座標を決める
                        spawnPosIdx = Random.Range(0, spawnPoint.Length);
                        spawnPosX = spawnPoint[spawnPosIdx].transform.position.x;
                        //敵を出す
                        StartCoroutine(delaySummonForMagicCircle(EnemyKind.dragon, spawnPosX));
                        dragonTimer.timer -= dragonTimer.interval;
                    }
                }
                //半分より少ないなら
                else
                {
                    //メイジスライムが一定間隔で出現

                    mageTimer.timer -= Time.deltaTime;

                    if (mageTimer.interval <= mageTimer.timer)
                    {
                        //出現する座標を決める
                        spawnPosIdx = Random.Range(0, spawnPoint.Length);
                        spawnPosX = spawnPoint[spawnPosIdx].transform.position.x;
                        //敵を出す
                        StartCoroutine(delaySummonForMagicCircle(EnemyKind.mage, spawnPosX));
                        mageTimer.timer -= mageTimer.interval;
                    }
                }
                break;
        }


    }

    private void setKindForProbability(int slimeProb, int shieldProb, int golemProb, int mageProb, int dragonProb)
    {
        probabilityNum = Random.Range(0, 99);

        int num0 = slimeProb;
        int num1 = num0 + shieldProb;
        int num2 = num1 + golemProb;
        int num3 = num2 + mageProb;
        int num4 = num3 + dragonProb;

        if (num4 != 100) Debug.LogError("確率が100でない");

        if (0 <= probabilityNum && probabilityNum <num0) kind = EnemyKind.slime;
        if (num0 <= probabilityNum && probabilityNum < num1) kind = EnemyKind.shield;
        if (num1 <= probabilityNum && probabilityNum < num2) kind = EnemyKind.golem;
        if (num2 <= probabilityNum && probabilityNum < num3) kind = EnemyKind.mage;
        if (num3 <= probabilityNum && probabilityNum < num4) kind = EnemyKind.dragon;
    }

    private IEnumerator level1HalfHpEvent()
    {
        StartCoroutine(delaySummonForMagicCircle(EnemyKind.dragon, spawnPoint[4].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.slime, spawnPoint[3].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.shield, spawnPoint[2].transform.position.x));
    }

    private IEnumerator level2HalfHpEvent()
    {
        StartCoroutine(delaySummonForMagicCircle(EnemyKind.golem, spawnPoint[4].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.slime, spawnPoint[3].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.shield, spawnPoint[2].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.dragon, spawnPoint[1].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.mage, spawnPoint[4].transform.position.x));

    }

    private IEnumerator level3HalfHpEvent()
    {
        StartCoroutine(delaySummonForMagicCircle(EnemyKind.golem, spawnPoint[4].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.shield, spawnPoint[3].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.shield, spawnPoint[2].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.dragon, spawnPoint[1].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.golem, spawnPoint[0].transform.position.x));

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(delaySummonForMagicCircle(EnemyKind.mage, spawnPoint[4].transform.position.x));
    }
}
