using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalBullet : MonoBehaviour
{

    enum Level
    {
        first,
        second,
        third
    }

    //(���Ȃ�-1�E�Ȃ�1��Ȃ�0, ��Ȃ�1����ȊO��0)
    protected Vector2 direction = new (1,0);
    private float speed=10;
    private int power=10;
    private bool isShot=false;
    private float chargeTimer=0;
    private float chargeTime2nd=1.5f;
    private float chargeTime3rd=3.0f;
    private Level level = Level.first;
    private bool flagDestroyOnce=false;
    private bool isPenetrate;

    // Start is called before the first frame update
    void Start()
    {
        changeDir();
    }
    void Update()
    {
        //�e���˔���
        if (Input.GetKeyUp(KeyCode.Return))
        {
            isShot = true;
        }
        //���ˑO
        if(!isShot)
        {
            //���x������
            chargeTimer += Time.deltaTime;
            if (chargeTime3rd < chargeTimer)
            {
                level = Level.third;
            }
            else if(chargeTime2nd < chargeTimer)
            {
                level = Level.second;
            }

            //���x���ʏ���
            if (level == Level.first)
            {
                speed = 10f;
                power = 10;
                isPenetrate = false;
            }
            else if(level == Level.second)
            {
                power = 50;
                speed = 25f;
                isPenetrate = true;
                this.transform.localScale = new(1.05f, 1.05f, 1);
            }
            else if(level == Level.third) 
            {
                power = 200;
                speed = 8f;
                isPenetrate = false;
                
                this.transform.localScale = new(1.3f, 1.3f, 1);
            }
            changeDir();
            move();
            Debug.Log("Timer=" + chargeTimer) ;
        }
        //���ˌ�
        else
        {
            if (!flagDestroyOnce)
            {
                Destroy(this.gameObject, 2);
                flagDestroyOnce = true;
            }
            shot();
        }
    }

    private void shot()
    {
        transform.position += new Vector3(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);
    }

    private void changeDir()
    {
        if (GameObject.Find("Player").GetComponent<move>().right)
        {
            direction = new(1, 0);
        }
        else
        {
            direction = new(-1, 0);
        }
        if (GameObject.Find("Player").GetComponent<move>().up)
        {
            direction = new(0, 1);
        }
    }

    private void move()
    {
        //�E
        if (GameObject.Find("Player").GetComponent<move>().right) {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(1, 0, 0) ;
        }
        //��
        else if(!GameObject.Find("Player").GetComponent<move>().right)
        {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(-1, 0, 0);
        }
        //��
        if (GameObject.Find("Player").GetComponent<move>().up)
        {
            transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 1, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemy>().damage(power);
            
        }        
    }

    public void destroy()
    {
        if (!isPenetrate)
        {
            Destroy(this.gameObject);
        }
    }

}
