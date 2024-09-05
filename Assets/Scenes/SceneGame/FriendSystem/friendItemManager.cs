using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendItemManager : MonoBehaviour
{
    public friendItemBox[] itemScripts;

    //アイテムを格納、格納出来たらtrue、できなかったらfalseを返す
    public bool getItem(FriendType type)
    {
        for(int i=0;i<itemScripts.Length; i++)
        {
            //アイテムが埋まっていなかったら
            if (!itemScripts[i].existItem)
            {
                //ボックスにアイテムを格納
                itemScripts[i].getFriend(type);
                //格納成功
                return true;
            }
        }
        //格納失敗
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
