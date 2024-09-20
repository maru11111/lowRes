using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class castleHpBar : MonoBehaviour
{
    public castle castleScript;
    private int maxHp;
    private int currentHp;
    public Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        updateHp();
    }

    // Update is called once per frame
    void Update()
    {
        updateHp();
        hpBar.fillAmount = (float)currentHp / (float)maxHp;
    }
    private void updateHp()
    {
        if (castleScript != null)
        {
            maxHp = castleScript.maxHp;
            currentHp = castleScript.currentHp;
        }
        else
        {
            Debug.Log("baseEnemyが見つかりませんでした");
        }
    }
}