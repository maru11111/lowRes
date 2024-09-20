using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonParam : MonoBehaviour
{
    public enum StageLevel
    {
        Level1,
        Level2,
        Level3,
        NotSelected
    }
    public static int playerPoint;
    public static int playerPowerUpgradeLevel;
    public static int playerHPUpgradeLevel;
    public static int castleHpUpgradeLevel;
    public static int friendStatusUpgradeLevel;
    public static int friendPopProbabilityUpgradeLevel;
    public static bool onDoubleJump;
    public static bool onRolling;
    public static List<spawnEnemy.EnemyKind> initialFriendKinds;
    public static StageLevel selectStageLevel;
    public static int SRankBorder1 = 1000;
    public static int SRankBorder2 = 2000;
    public static int SRankBorder3 = 3000;
    public static int ARankBorder1 = 666;
    public static int ARankBorder2 = 1333;
    public static int ARankBorder3 = 2000;
    public static int BRankBorder1 = 333;
    public static int BRankBorder2 = 666;
    public static int BRankBorder3 = 1000;
}