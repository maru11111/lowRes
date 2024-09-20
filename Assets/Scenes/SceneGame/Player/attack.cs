using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class attack : MonoBehaviour
{

    [SerializeField] GameObject prefab;
    public rolling rollingScript;
    public pause pauseScript;
    public player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Player"). GetComponent<takeDamage>().isStop && !rollingScript.isRolling && !pauseScript.isPause && !playerScript.isDead && !playerScript.isGameSet)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (normalBullet.bulletInstanceNum < normalBullet.bulletInstanceMaxNum)
                {
                    Instantiate(prefab, GameObject.Find("Player").transform.position, Quaternion.identity);
                }
            }
        }


    }

}