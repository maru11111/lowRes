using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    [SerializeField] GameObject prefab;
    public rolling rollingScript;
    public pause pauseScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Player"). GetComponent<takeDamage>().isStop && !rollingScript.isRolling && !pauseScript.isPause)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Instantiate(prefab, GameObject.Find("Player").transform.position, Quaternion.identity);
            }
        }


    }

}