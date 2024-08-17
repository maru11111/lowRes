using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody2D player;
    private float jumpSpeed = 200f;
    private bool onFloor;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ínñ Ç…Ç¢ÇΩÇÁÉWÉÉÉìÉv
        if (onFloor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                onFloor = false;
                player.AddForce(new Vector2(0f, jumpSpeed));
            }
        }
        Debug.Log(onFloor);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //è∞Ç∆ê⁄êGÇµÇƒÇ¢ÇÈÇ©îªíË
        if (collision.CompareTag("Stage"))
        {
            onFloor = true;
        }
    }
}
