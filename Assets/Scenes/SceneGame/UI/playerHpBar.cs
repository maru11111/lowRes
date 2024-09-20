using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playerHpBar : MonoBehaviour
{
    private int maxHp;
    private int currentHp;
    private Image hpBar;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = GetComponent<Image>();
        player = GameObject.Find("Player");
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
        if (player != null)
        {
            maxHp = player.GetComponent<player>().getMaxHp();
            currentHp = player.GetComponent<player>().currentHp;
        }
        else
        {
            Debug.Log("Playerが見つかりませんでした");
        }
    }
}
