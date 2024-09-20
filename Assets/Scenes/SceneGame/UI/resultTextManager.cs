using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class resultTextManager : MonoBehaviour
{
    public TextMeshProUGUI victoryOrDefeatText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI missionDefendText;
    public TextMeshProUGUI missionDeathText;
    public TextMeshProUGUI missionBreakText;
    public TextMeshProUGUI rankText;

    public GameObject victoryOrDefeat;
    public GameObject rank;
    public GameObject missionDefend;
    public GameObject missionDeath;
    public GameObject missionBreak;
    public GameObject backGround;
    public GameObject backToTitle;

    private Vector3 victoryOrDefeatPos1 = new Vector3(0f, 47f);
    private Vector3 victoryOrDefeatPos2 = new Vector3(0f, 230f + 19f+18f);
    private Vector3 rankPos = new Vector3(326f, 60.5f);
    private Vector3 missionDefendPos = new Vector3(-364f, 120f);
    private Vector3 missionDeathPos = new Vector3(-351f, 9f);
    private Vector3 missionBreakPos = new Vector3(-373.5f, -103f);
    private Vector3 backGroundPos = new Vector3(43, 9f);
    private float backToTitlePos = 445;

    public AudioSource audioFail;
    public AudioSource audioClear1;
    public AudioSource audioClear2;
    public AudioSource audioClear3;
    public AudioSource audioC_Rank;
    public AudioSource audioB_Rank;
    public AudioSource audioA_Rank;
    public AudioSource audioS_Rank;
    public AudioSource BGMFail;
    public AudioSource BGMClear;

    private bool flagDefend=false;
    private bool flagDeath=false;
    private bool flagBreak = false;
    private bool flagRank=false;
    private bool isBGMOn=false;

    public judgeGameResult gameResult;

    private float timer=0;
    private int missionClearCount=0;

    // Start is called before the first frame update
    void Start()
    {
        //勝ったか負けたか
        if (gameResult.isPlayerWin)
        {
            victoryOrDefeatText.text = "勝利！！";
        }
        else
        {
            victoryOrDefeatText.text = "敗北...";
        }

        //ミッション達成状況
        if (gameResult.isNeverDied)
        {
            missionDeathText.color = new Color(1f,1f,1f);
        }
        if (gameResult.isDefendCastle)
        {
            missionDefendText.color = new Color(1f, 1f, 1f);
        }
        if(gameResult.isBreakCastle)
        {
            missionBreakText.color = new Color(1f, 1f, 1f);
        }

        //スコア
        scoreText.text = "スコア:"+gameResult.score.ToString();

        //ランク
        switch (gameResult.rank)
        {
            case judgeGameResult.Rank.C: 
                rankText.text = "C";
                rankText.color = new Color(37f/255f, 224f/225f, 87f/225f);
                break;

            case judgeGameResult.Rank.B: 
                rankText.text = "B";
                rankText.color = new Color(60f/255f, 95f/255f, 255f/255f);
                break;

            case judgeGameResult.Rank.A: 
                rankText.text = "A";
                rankText.color = new Color(241f/255f,54f/255f,53f/255f); 
                break;

            case judgeGameResult.Rank.S: 
                rankText.text = "S";
                rankText.color = new Color(255f/255f, 212f/255f, 98f/255f); 
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 1.5f)
        {
            victoryOrDefeat.transform.DOLocalMove(victoryOrDefeatPos1, 1f);
        }
        if (2f < timer)
        {
            victoryOrDefeat.transform.DOLocalMove(victoryOrDefeatPos2, 2f);
        }
        if (2.2f < timer)
        {
            backGround.transform.DOLocalMove(backGroundPos, 1f);
        }
        if (3.6f < timer)
        {
            if (!flagDefend)
            {
                if (gameResult.isDefendCastle)
                {
                    audioClear1.Play();
                    missionClearCount++;
                }
                else
                {
                    audioFail.Play();
                }
                flagDefend = true;
            }
            missionDefend.transform.DOLocalMove(missionDefendPos, 1f);
        }
        if (4.6f < timer)
        {
            if (!flagDeath)
            {
                if (gameResult.isNeverDied)
                {
                    if (missionClearCount == 0) audioClear1.Play();
                    if (missionClearCount == 1) { audioClear2.Play(); }
                    missionClearCount++;
                }
                else
                {
                    audioFail.Play();
                }
                flagDeath = true;
            }
            missionDeath.transform.DOLocalMove(missionDeathPos, 1f);
        }
        if (5.6f < timer)
        {
            if (!flagBreak)
            {
                if (gameResult.isBreakCastle)
                {
                    if (missionClearCount == 0) audioClear1.Play();
                    if (missionClearCount == 1) audioClear2.Play();
                    if (missionClearCount == 2) audioClear3.Play();
                }
                else
                {
                    audioFail.Play();
                }
                flagBreak = true;
            }
            missionBreak.transform.DOLocalMove(missionBreakPos, 1f);
        }
        if (6.6f < timer)
        {
            if (!flagRank)
            {
                switch (gameResult.rank)
                {
                    case judgeGameResult.Rank.C:
                        audioC_Rank.Play();
                        break;

                    case judgeGameResult.Rank.B:
                        audioB_Rank.Play();
                        break;

                    case judgeGameResult.Rank.A:
                        audioA_Rank.Play();
                        break;

                    case judgeGameResult.Rank.S:
                        audioS_Rank.Play();
                        break;
                }

                flagRank = true;
            }
            rank.transform.DOLocalMove(rankPos, 1f);
        }
        if (7.6f < timer)
        {
            backToTitle.transform.DOLocalMoveX(backToTitlePos, 1f);

            //スペースキーでタイトルに戻る
            if (Input.GetKey(KeyCode.Space)){
                SceneManager.LoadScene("SceneHome");
            }

            if (!isBGMOn)
            {
                if (gameResult.isPlayerWin)
                {
                    BGMClear.Play();
                }
                else
                {
                    BGMFail.Play();
                }
                isBGMOn = true;
            }

        }
    }
}
