using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling : MonoBehaviour
{
    public Collider2D playerCollider;
    public GameObject playerColliderObj;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public jump jumpScript;
    public takeDamage takeDamageScript;
    public pause pauseScript;
    public player playerScript;
    public bool isRolling=false;
    public Rigidbody2D player;
    private float maxSpeed = 8f;
    private float currentSpeed = 0;
    public bool moving=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void isRolligOff()
    {
        isRolling = false;
    }

    private void offCollider()
    {
        playerCollider.enabled = false;
    }

    private void onCollider()
    {
        playerCollider.enabled = true;
    }

    private void changeLayerToRolling()
    {
        playerColliderObj.layer = LayerMask.NameToLayer("PlayerPhysicsForRolling");
    }

    private void changeLayerToNormal()
    {
        playerColliderObj.layer = LayerMask.NameToLayer("PlayerPhysics");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isRolling" + isRolling);

        if (!takeDamageScript.isStop && !jumpScript.isJumping && !pauseScript.isPause && !playerScript.isDead && !playerScript.isGameSet && SaveDataManager.data.onRolling == 1)
        {
            if (!isRolling)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    anim.Play("Rolling");
                    player.velocity = Vector3.zero;
                    currentSpeed = maxSpeed;
                    //右を向いていたら
                    if (spriteRenderer.flipX)
                    {
                        player.velocity = new Vector2(currentSpeed, 0f);
                    }
                    //左
                    else
                    {
                        player.velocity = new Vector2 (-currentSpeed, 0f);
                    }
                    isRolling = true;
                    moving = true;
                }

            }
            else
            {
                Debug.Log("vel" + currentSpeed);
                //右を向いていたら
                if (spriteRenderer.flipX)
                {
                    player.velocity = new Vector2(currentSpeed, 0f);
                }
                //左
                else
                {
                    player.velocity = new Vector2(-currentSpeed, 0f);
                }
                currentSpeed *= 0.99f;
            }
        }

    }
}
