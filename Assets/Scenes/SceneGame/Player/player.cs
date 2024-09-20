using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    public Animator anim;
    public AudioSource stageAudio;
    public AudioSource deathSe;
    public Rigidbody2D playerRigid;
    public GameObject mainCamera;
    public GameObject subCamera;
    public GameObject PlayerUICanvas;
    public GameObject ItemBoxCanvas;
    public GameObject PlayerDeadCanvas;
    public textCountDownTime textCountDownTimeScript;
    public PlayerInput playerInput;
    private int maxHp=100;
    public int currentHp;
    private float timer=0;
    private float interval=1.5f;
    private bool muteki=false;
    public bool isDead = false;
    public float deadHitstopTime = 0.2f;
    private Vector2 playerStartPos = new Vector2 (-12f, -3.331519f);
    private float prebVolume;
    public float prevPlayTime = 0;
    public int deathCount=0;
    public bool isGameSet=false;

    //眷属の出現確率(％)
    private int probability = 10;

    // Start is called before the first frame update
    void Start()
    {

        
        //HPレベル補正
        if (SaveDataManager.data.playerHPLevel == 0)
        {
            //変化なし
        }
        else if(SaveDataManager.data.playerHPLevel == 1)
        {
            maxHp += 10;

        }else if(SaveDataManager.data.playerHPLevel == 2)
        {
            maxHp += 20;

        }else if(SaveDataManager.data.playerHPLevel == 3)
        {
            maxHp += 30;
        }

        //眷属のドロップ率レベル補正
        if (SaveDataManager.data.friendDropLevel == 0)
        {
            //変化なし
        }
        else if (SaveDataManager.data.friendDropLevel == 1)
        {
            probability += 5;
        }
        else if (SaveDataManager.data.friendDropLevel == 2)
        {
            probability += 10;
        }
        else if (SaveDataManager.data.friendDropLevel == 3)
        {
            probability += 15;
        }

        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //死亡時処理
        if (currentHp <= 0)
        {
            //死んだときに一回だけ
            if (!isDead)
            {
                //死亡SE
                deathSe.Play();
                //ヒットストップ
                hitStop.hitStopInstance.startHitStop(deadHitstopTime);
                //BGMの再生時間を保存
                prevPlayTime = stageAudio.time;
                //BGMストップ
                stageAudio.Stop();
                //ヒットストップだけBGMをストップさせるコルーチン
                StartCoroutine(stopAudio());
                //死亡アニメーション
                anim.SetTrigger("Die");
                //ダメージアニメーションをオフ
                anim.SetBool("TakeDamage", false);
                //ボリュームを保存
                prebVolume = stageAudio.volume;
                //ボリュームを下げる
                stageAudio.volume = 0.3f;
                //速度を0に
                playerRigid.velocity = Vector3.zero;
                //レイヤー変更
                this.gameObject.layer = LayerMask.NameToLayer("DeadPlayer");
                //死亡回数をカウント
                deathCount++;
                //アクションマップを変更
                playerInput.SwitchCurrentActionMap("NoAct");
                //死亡フラグをオン
                isDead = true;
            }
        }
        //復活処理
        if (textCountDownTimeScript.isWaitEnd)
        {
            stageAudio.volume = prebVolume;
            currentHp = maxHp;
            PlayerDeadCanvas.SetActive(false);
            PlayerUICanvas.SetActive(true);
            ItemBoxCanvas.GetComponent<Canvas>().enabled = true;
            subCamera.SetActive(false);
            mainCamera.SetActive(true);
            textCountDownTimeScript.isWaitEnd = false;
            this.gameObject.layer = LayerMask.NameToLayer("Player");
            this.transform.position = playerStartPos;
            anim.Play("Idle");
            playerInput.SwitchCurrentActionMap("Action");
            isDead = false;
        }


        if (muteki)
        {
            timer += Time.deltaTime;
            if (interval < timer)
            {
                muteki = false;
                timer = 0;
            }
        }
    }

    public int getMaxHp()
    {
        return maxHp;
    }

    public int getProbability()
    {
        return probability;
    }

    private void changeSubCamera()
    {
        PlayerDeadCanvas.SetActive(true);
        textCountDownTimeScript.resetTimer();
        PlayerUICanvas.SetActive(false);
        ItemBoxCanvas.GetComponent<Canvas>().enabled = false;
        mainCamera.SetActive(false);
        subCamera.SetActive(true);
    }

    private IEnumerator stopAudio()
    {
        yield return new WaitForSeconds(deadHitstopTime);
        stageAudio.time = prevPlayTime;
        stageAudio.Play();
    }

    public void damage(int damage, Vector2 effectPos, GameObject otherObj)
    {
        if (!muteki)
        {
            currentHp -= damage;

            //吹っ飛ばし方向
            int direction;
            //プレイヤーの右側にぶつかれば
            if (otherObj.transform.position.x <= this.transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            //吹っ飛ばし
            GetComponent<takeDamage>().startKnockBack(direction);

            //衝突点にダメージエフェクト
            GameObject.Find("EffectScript").GetComponent<damageEffect>().damageEffectPlay(effectPos);
        
        //マイナスにならないようにする
        if (currentHp < 0)
            {
                currentHp = 0;
            }

            muteki = true;
        }
    }
}
