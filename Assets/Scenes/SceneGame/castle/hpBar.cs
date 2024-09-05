using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    Slider slider;
    int castleHp;

    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        var castle = GetComponentInParent<castle>();
        slider.maxValue = castle.hp;
    }

    // Update is called once per frame
    void Update()
    {
        //hpを取得
        castleHp = GetComponentInParent<castle>().hp;

        //スライダーを更新
        slider.value = (float)castleHp;
    }
}
