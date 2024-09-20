using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class castle : MonoBehaviour
{
    public int maxHp = 200;
    public int currentHp;
    public bool isBroken=false;
    public bool isPlayerCastle = false;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー城なら
        //ショップ補正を加える
        if (isPlayerCastle)
        {
            if(SaveDataManager.data.castleHPLevel == 0)
            {
                //何もしない
            }
            else if(SaveDataManager.data.castleHPLevel == 1)
            {
                maxHp += 100;
            }
            else if(SaveDataManager.data.castleHPLevel == 2)
            {

                maxHp += 150;
            }
            else if(SaveDataManager.data.castleHPLevel == 3)
            {
                maxHp += 200;
            }
        }
        //敵城なら
        else
        {
            if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level1)
            {
                maxHp = 2000;
            }
            if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level2)
            {
                maxHp = 2500;
            }
            if (CommonParam.selectStageLevel == CommonParam.StageLevel.Level3)
            {
                maxHp = 3000;
            }
        }
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //城が壊されたか判定
        if(currentHp <= 0)
        {
            isBroken = true;
        }
    }

    public void damage(int damage , Vector2 effectPos)
    {
        currentHp -= damage;

        //衝突点にダメージエフェクト
        GameObject.Find("EffectScript").GetComponent<damageEffect>().damageEffectPlay(effectPos);
    }

}
