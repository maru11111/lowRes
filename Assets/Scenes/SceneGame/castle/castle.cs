using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class castle : MonoBehaviour
{

    public int hp=200;
    private bool isDefeat=false;

    // Start is called before the first frame update
    void Start()
    {
        hp = 200;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオーバー判定
        if(hp < 0)
        {
            isDefeat = true;
        }
    }

    public void damage(int damage)
    {
        hp -= damage;
    }

}
