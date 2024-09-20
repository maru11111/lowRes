using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody2D player;
    public rolling rollingScript;
    public takeDamage takeDamageScript;
    public pause pauseScript;
    public player playerScript;
    private float jumpSpeed = 220f;
    private bool onFloor;
    public Animator anim;
    public bool isJumping=false;
    public int maxJump=2;
    public int jumpCount=0;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //ダメージ硬直中でないかつローリング中でなければ
        if (!takeDamageScript.isStop && !rollingScript.isRolling && !pauseScript.isPause && !playerScript.isDead && !playerScript.isGameSet)
        {
            Debug.Log("Onfloor"+ onFloor);

            //地面にいたら
            if (onFloor)
            {
                //床にいるなら落下アニメーションオフ
                anim.SetBool("Fall", false);
                //ダッシュ中でないかつローリング中でないなら待機
                if (!anim.GetBool("Dash") && !rollingScript.isRolling)
                {
                    anim.Play("Idle");
                }
                isJumping = false;
                jumpCount = 0;

                if (Input.GetKeyDown(KeyCode.A))
                {
                    //ジャンプスタート
                    onFloor = false;
                    player.AddForce(new Vector2(0f, jumpSpeed));
                    anim.Play("Jump");
                    anim.SetBool("Dash", false);
                    isJumping = true;
                    jumpCount++;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A) && SaveDataManager.data.onDoubleJump == 1)
                {
                    //二段ジャンプ回数内なら
                    if (jumpCount < maxJump)
                    {
                        player.velocity = new Vector2(player.velocity.x, 0);
                        player.AddForce(new Vector2(0f, jumpSpeed));
                        anim.SetBool("Fall", false);
                        anim.Play("Jump");
                        jumpCount++;
                    }
                }

                //y方向の速度がマイナスになったら(なぜか<0だと上手く動かない)
                if (player.velocity.y < -0.1f)
                {
                    //落下アニメーション
                    
                    anim.SetBool("Fall", true);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //床と接触しているか判定
        if (collision.CompareTag("Stage"))
        {
            onFloor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //床と接触しているか判定
        if (collision.CompareTag("Stage"))
        {
            onFloor = false;
        }
    }
}
