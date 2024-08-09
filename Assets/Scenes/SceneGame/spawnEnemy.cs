using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    [SerializeField]GameObject prefab;
    
    private float interval=2;

    private float timer=0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (interval < timer)
        {
            Instantiate(prefab,new Vector3(3.56f,0,0), Quaternion.identity);
            timer -= interval;
        }
        
    }
}
