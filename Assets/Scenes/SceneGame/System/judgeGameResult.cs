using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class judgeGameResult : MonoBehaviour
{
    public enum Rank
    {
        S,
        A,
        B,
        C
    }

    public player playerScript;
    public castle playerCastleScript;
    public castle enemyCastleScript;
    public timerText timerTextScript;

    public bool isNeverDied = false;
    public bool isBreakCastle = false;
    public bool isDefendCastle = false;

    private int scoreNeverDied1 = 100;
    private int scoreNeverDied2 = 200;
    private int scoreNeverDied3 = 300;
    private int scoreNeverDiedAndDefend1OrBreak1 = 233;
    private int scoreNeverDiedAndDefend1OrBreak2 = 566;
    private int scoreNeverDiedAndDefend1OrBreak3 = 900;
    private int scoreBreakCastle1 = 233+300;
    private int scoreBreakCastle2 = 566+300;
    private int scoreBreakCastle3 = 900+300;
    private int scoreDefendCastle1 = 233;
    private int scoreDefendCastle2 = 566;
    private int scoreDefendCastle3 = 900;

    private int scoreRemainingTimePerSecond1 = 5;
    private int scoreRemainingTimePerSecond2 = 10;
    private int scoreRemainingTimePerSecond3 = 15;

    public bool isPlayerWin = false;
    public bool isPlayerDefeat = false;
    public bool isJudgeEnd=false;

    public int score = 0;
    public Rank rank;

    public SaveDataManager saveDataManager;

    // Update is called once per frame
    void Update()
    {
        if (!isJudgeEnd)
        {
            //敗北したか
            if (playerCastleScript.isBroken)
            {
                isPlayerDefeat = true;
                common();
                isJudgeEnd = true;

            }
            //勝利したか
            if (timerTextScript.isTimeLimit || enemyCastleScript.isBroken)
            {
                isPlayerWin = true;
                common();
                checkStageClear();
                isJudgeEnd = true;
            }
        }
    }

    private void common()
    {
        checkMission();
        CalcScore();
        setRank();
        //スコアをポイントに加算
        SaveDataManager.data.playerPoint += score;
        //ハイスコアを更新
        switch (CommonParam.selectStageLevel)
        {
            case CommonParam.StageLevel.Level1:
                if (SaveDataManager.data.stage1HighScore < score) SaveDataManager.data.stage1HighScore = score;
                break;

            case CommonParam.StageLevel.Level2:
                if (SaveDataManager.data.stage2HighScore < score) SaveDataManager.data.stage2HighScore = score;
                break;

            case CommonParam.StageLevel.Level3:
                if (SaveDataManager.data.stage3HighScore < score) SaveDataManager.data.stage3HighScore = score;
                break;
        }
        //セーブ
        saveDataManager.Save();       
    }

    //ステージをクリアしているかとその処理
    private void checkStageClear()
    {
        //通常クリア
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level1 && CommonParam.ARankBorder1 <= score) SaveDataManager.data.isStage1Clear=1;
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level2 && CommonParam.ARankBorder2 <= score) SaveDataManager.data.isStage2Clear=1;
        //パーフェクトクリア
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level1 && CommonParam.SRankBorder1 <= score) SaveDataManager.data.isStage1PerfectClear = 1;
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level2 && CommonParam.SRankBorder2 <= score) SaveDataManager.data.isStage2PerfectClear = 1;
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level3 && CommonParam.SRankBorder3 <= score) SaveDataManager.data.isStage3PerfectClear = 1;
        //セーブ
        saveDataManager.Save();
    }

    private void setRank()
    {
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level1)
        {
            if (CommonParam.SRankBorder1 <= score)
            {
                rank = Rank.S;
            }
            else if (CommonParam.ARankBorder1 <= score)
            {
                rank = Rank.A;
            }
            else if (CommonParam.BRankBorder1 <= score)
            {
                rank = Rank.B;
            }
            else if (0 <= score)
            {
                rank = Rank.C;
            }
        }
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level2)
        {
            if (CommonParam.SRankBorder2 <= score)
            {
                rank = Rank.S;
            }
            else if (CommonParam.ARankBorder2 <= score)
            {
                rank = Rank.A;
            }
            else if (CommonParam.BRankBorder2 <= score)
            {
                rank = Rank.B;
            }
            else if (0 <= score)
            {
                rank = Rank.C;
            }
        }
        if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level3)
        {
            if (CommonParam.SRankBorder3 <= score)
            {
                rank = Rank.S;
            }
            else if (CommonParam.ARankBorder3 <= score)
            {
                rank = Rank.A;
            }
            else if (CommonParam.BRankBorder3 <= score)
            {
                rank = Rank.B;
            }
            else if (0 <= score)
            {
                rank = Rank.C;
            }
        }
    }

    private void checkMission()
    {
        //一度も死んでない
        if(playerScript.deathCount == 0)
        {
            isNeverDied = true;
        }
        //敵城を壊した
        if (enemyCastleScript.isBroken)
        {
            isBreakCastle = true;
        }
        //自城が壊されてない
        if (!playerCastleScript.isBroken)
        {
            isDefendCastle= true;
        }
    }

    private void CalcScore()
    {
        switch (CommonParam.selectStageLevel)
        {
            case CommonParam.StageLevel.Level1:
                //ミッションのスコア
                if (isNeverDied)
                {
                    score += scoreNeverDied1;
                }
                if (isBreakCastle)
                {
                    score += scoreBreakCastle1;
                }
                if (isDefendCastle)
                {
                    score += scoreDefendCastle1;
                }

                //城を壊したときの追加スコア
                if (isBreakCastle)
                {
                    //残り時間多いほどスコアを追加
                    score += (int)timerTextScript.currentRemainingTime * scoreRemainingTimePerSecond1;
                    //城を壊したかつ一度も死んでなかったらさらに追加スコア
                    if (isNeverDied)
                    {
                        score += scoreNeverDiedAndDefend1OrBreak1;
                    }
                }
                //城を壊されてしまった時の追加スコア
                if (isPlayerDefeat)
                {
                    //生き延びた時間が長いほどスコアを追加
                    score += (int)((timerTextScript.timer / timerTextScript.getTimeLimit()) * 333);
                }
                //防衛だけしたときの追加スコア
                if (isDefendCastle && !isBreakCastle)
                {
                    //防衛かつ一度も死んでなかったらさらに追加スコア
                    if (isNeverDied)
                    {
                        score += scoreNeverDiedAndDefend1OrBreak1;
                    }
                    //敵の城を削っただけ追加スコア
                    score += (int)((1.0f - ((float)enemyCastleScript.currentHp / (float)enemyCastleScript.maxHp)) * 333);
                }
                break;

            case CommonParam.StageLevel.Level2:
                //ミッションのスコア
                if (isNeverDied)
                {
                    score += scoreNeverDied2;
                }
                if (isBreakCastle)
                {
                    score += scoreBreakCastle2;
                }
                if (isDefendCastle)
                {
                    score += scoreDefendCastle2;
                }

                //城を壊したときの追加スコア
                if (isBreakCastle)
                {
                    //残り時間多いほどスコアを追加
                    score += (int)timerTextScript.currentRemainingTime * scoreRemainingTimePerSecond2;
                    //城を壊したかつ一度も死んでなかったらさらに追加スコア
                    if (isNeverDied)
                    {
                        score += scoreNeverDiedAndDefend1OrBreak2;
                    }
                }
                //城を壊されてしまった時の追加スコア
                if (isPlayerDefeat)
                {
                    //生き延びた時間が長いほどスコアを追加
                    score += (int)((timerTextScript.timer / timerTextScript.getTimeLimit()) * 666);
                }
                //防衛だけしたときの追加スコア
                if (isDefendCastle && !isBreakCastle)
                {
                    //防衛かつ一度も死んでなかったらさらに追加スコア
                    if (isNeverDied)
                    {
                        score += scoreNeverDiedAndDefend1OrBreak2;
                    }
                    //敵の城を削っただけ追加スコア
                    score += (int)((1.0f - ((float)enemyCastleScript.currentHp / (float)enemyCastleScript.maxHp)) * 666);
                }
                break;

            case CommonParam.StageLevel.Level3:
                //ミッションのスコア
                if (isNeverDied)
                {
                    score += scoreNeverDied3;
                }
                if (isBreakCastle)
                {
                    score += scoreBreakCastle3;
                }
                if (isDefendCastle)
                {
                    score += scoreDefendCastle3;
                }

                //城を壊したときの追加スコア
                if (isBreakCastle)
                {
                    //残り時間多いほどスコアを追加
                    score += (int)timerTextScript.currentRemainingTime * scoreRemainingTimePerSecond3;
                    //城を壊したかつ一度も死んでなかったらさらに追加スコア
                    if (isNeverDied)
                    {
                        score += scoreNeverDiedAndDefend1OrBreak3;
                    }
                }
                //城を壊されてしまった時の追加スコア
                if (isPlayerDefeat)
                {
                    //生き延びた時間が長いほどスコアを追加
                    score += (int)((timerTextScript.timer / timerTextScript.getTimeLimit()) * 999);
                }
                //防衛だけしたときの追加スコア
                if (isDefendCastle && !isBreakCastle)
                {
                    //防衛かつ一度も死んでなかったらさらに追加スコア
                    if (isNeverDied)
                    {
                        score += scoreNeverDiedAndDefend1OrBreak3;
                    }
                    //敵の城を削っただけ追加スコア
                    score += (int)((1.0f - ((float)enemyCastleScript.currentHp / (float)enemyCastleScript.maxHp)) * 999);
                }
                break;
        }
    }
}
