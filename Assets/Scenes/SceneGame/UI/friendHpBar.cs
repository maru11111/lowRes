using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class friendHpBar : MonoBehaviour
{
    private int maxHp;
    private int currentHp;
    private Image hpBar;
   
    // Start is called before the first frame update
    void Start()
    {
        hpBar = GetComponent<Image>();
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
        var friend = GetComponentInParent<baseEnemy>();

        if (friend != null)
        {
            maxHp = friend.maxHp;
            currentHp = friend.currentHp;
        }
        else
        {
            Debug.Log("baseEnemyが見つかりませんでした");
        }
    }
}
