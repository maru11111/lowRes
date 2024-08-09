using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//test
public class enemy : MonoBehaviour
{

    private int hp = 30;
    private int power = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void damage(int damage)
    {
        hp -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //’e‚É“–‚½‚ê‚Îdestroyˆ—
        if(collision.gameObject.tag == "Bullet")
        {
            collision.GetComponent<NormalBullet>().destroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<player>().damage(power);
        }
    }
}
