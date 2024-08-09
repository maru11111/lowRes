using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //íeÇÃë¨Ç≥
    protected float speed;
    //íeÇÃå¸Ç´
    protected float direction;
    //É_ÉÅÅ[ÉWÇÃëÂÇ´Ç≥
    protected float power;
    //

    // Start is called before the first frame update
    void Start()
    {
        direction = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        move();

    }

    public void move()
    {

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = -1f;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = 1f;
        }

        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
    }

    public void remove()
    {
        Destroy(this.gameObject);
    }
    
}
