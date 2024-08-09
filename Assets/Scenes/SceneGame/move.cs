using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class move : MonoBehaviour
{

    Rigidbody2D player;
    private float speed = 3f;
    private float currentSpeed;
    public bool right = false;
    public bool up = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ����x��ݒ�
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            currentSpeed = -speed;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            currentSpeed = speed;
        }
        else
        {
            currentSpeed = 0;
        }

        //������ς���
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            right = false;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            up = true;
            //�������
        }
        else
        {
            up = false;
        }

    }

    private void FixedUpdate()
    {
        //�ړ�
        player.velocity = (new Vector2(currentSpeed, player.velocity.y));
    }   
}