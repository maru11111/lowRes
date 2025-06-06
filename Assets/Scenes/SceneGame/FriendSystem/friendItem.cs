﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendItem : MonoBehaviour
{

    public FriendType friendType = FriendType.notExist;
    private friendItemManager friendItemManager;
    public bool takenSuccess = false;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    private void Start()
    {
        friendItemManager = GameObject.Find("FriendItemManager").GetComponent<friendItemManager>();
    }

    public void setParam(FriendType type)
    {
        friendType = type;
        switch (type)
        {
            case FriendType.slime:
                anim.Play("Slime");
                break;

            case FriendType.shield:
                anim.Play("Shield");
                break;

            case FriendType.golem:
                anim.Play("Golem");
                break;

            case FriendType.mage:
                anim.Play("Mage");
                break;

            case FriendType.dragon:
                anim.Play("Dragon");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (takenSuccess)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (friendType != FriendType.notExist)
        {
            if (collision.CompareTag("Player"))
            {
                takenSuccess = friendItemManager.getItem(friendType);
            }
        }
    }
}
