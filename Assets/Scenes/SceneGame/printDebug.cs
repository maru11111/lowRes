using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class printDebug : MonoBehaviour
{

    public TextMeshProUGUI t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t.text = "PlayerHp" + GameObject.Find("Player").GetComponent<player>().hp;
    }
}
