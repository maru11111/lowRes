using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public int playerPoint;
    public int playerHPLevel;
    public int playerPowerLevel;
    public int castleHPLevel;
    public int friendDropLevel;
    public int friendStrengthenLevel;
    //0ならオフ, 1ならオン
    public int onDoubleJump;
    public int onRolling;
    public int stage1HighScore;
    public int stage2HighScore;
    public int stage3HighScore;
    //0で未クリア, 1でクリア
    public int isStage1Clear;
    public int isStage2Clear;
    public int isStage1PerfectClear;
    public int isStage2PerfectClear;
    public int isStage3PerfectClear;
    //0で演出前, 1で演出後
    public int isStage1PerfectClearFirstFlag;
    public int isStage2PerfectClearFirstFlag;
    public int isStage3PerfectClearFirstFlag;
}
