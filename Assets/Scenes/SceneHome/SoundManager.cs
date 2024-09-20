using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SoundManager : MonoBehaviour
{
    private GameObject prevFocusObj;
    public GameObject stageObj;
    public GameObject shopObj;
    public GameObject stage1Obj;
    public GameObject stage2Obj;
    public GameObject stage3Obj;
    public GameObject playerHpObj;
    public GameObject powerObj;
    public GameObject castleHpObj;
    public GameObject friendDropObj;
    public GameObject friendStrengthenObj;

    public AudioSource focusAudio;
    public AudioSource changeStageOrShopAudio;
    public AudioSource submitAudio;
    public AudioSource powerUpAudio;
    public AudioSource failAudio;

    public ShopManager shopManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPressSE(InputAction.CallbackContext context)
    {
        //3回のコールバックのうちcanceledのとき
        if (context.started)
        {
            for (int i = 0; i < shopManagerScript.shopUI.Length; i++)
            {
                if (EventSystem.current.currentSelectedGameObject == shopManagerScript.shopUI[i].ButtonObj)
                {
                    if (shopManagerScript.shopUI[i].currentLevel == 0)
                    {
                        //買えれば
                        if (shopManagerScript.shopUI[i].requiredPointsUp1 <= SaveDataManager.data.playerPoint)
                        {
                            powerUpAudio.Play();
                            return;
                        }
                        else
                        {
                            failAudio.Play();
                            return;
                        }
                    }
                    else if (shopManagerScript.shopUI[i].currentLevel == 1)
                    {
                        if (shopManagerScript.shopUI[i].requiredPointsUp2 <= SaveDataManager.data.playerPoint)
                        {
                            powerUpAudio.Play();
                            return;
                        }
                        else
                        {
                            failAudio.Play();
                            return;
                        }
                    }
                    else if (shopManagerScript.shopUI[i].currentLevel == 2)
                    {
                        if (shopManagerScript.shopUI[i].requiredPointsUp3 <= SaveDataManager.data.playerPoint)
                        {
                            powerUpAudio.Play();
                            return;
                        }
                        else
                        {
                            failAudio.Play();
                            return;
                        }
                    }
                    else if (shopManagerScript.shopUI[i].currentLevel == 3)
                    {
                        failAudio.Play();
                        return;
                    }
                    else
                    {
                        failAudio.Play();
                        return;
                    }
                }
            }
            if(EventSystem.current.currentSelectedGameObject != stageObj && EventSystem.current.currentSelectedGameObject != shopObj)
            {
                submitAudio.Play();
            }
        }
    }

    public void focusSE(InputAction.CallbackContext context)
    {
        //canceledの方がなぜか先に呼ばれている...?

        //3回のコールバックのうちperformedのとき
        if (context.canceled)
        {
            Debug.Log("Performed:" + EventSystem.current.currentSelectedGameObject);
            if (prevFocusObj != EventSystem.current.currentSelectedGameObject)
            {
                if((EventSystem.current.currentSelectedGameObject ==stageObj && prevFocusObj == shopObj) ||(EventSystem.current.currentSelectedGameObject == shopObj && prevFocusObj ==stageObj))
                {
                    changeStageOrShopAudio.Play();
                }
                else
                {
                    focusAudio.Play();
                }
            }
        }

        //3回のコールバックのうちcanceledのとき
        if (context.performed)
        {
            Debug.Log("Canceled:" + EventSystem.current.currentSelectedGameObject);
            prevFocusObj = EventSystem.current.currentSelectedGameObject;

        }
    }
}
