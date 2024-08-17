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
        Debug.Log("maxValue"+slider.maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hp" + GetComponentInParent<castle>().hp);

        //hpを取得
        castleHp = GetComponentInParent<castle>().hp;

        //スライダーを更新
        slider.value = (float)castleHp;
    }
}
