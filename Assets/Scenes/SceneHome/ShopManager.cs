using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShopManager : MonoBehaviour
{
    public enum UIKind{
        PlayerHP,
        PlayerPower,
        CastleHP,
        FriendDrop,
        FriendStrengthen
    }

    [System.Serializable]
    public struct ShopUI {
        public string name;
        public UIKind kind;
        [SerializeField] public TextMeshProUGUI KindText;
        [SerializeField] public TextMeshProUGUI BuyText;
        [SerializeField] public GameObject ButtonObj;
        [HideInInspector]public int currentLevel;
        public int requiredPointsUp1;
        public int requiredPointsUp2;
        public int requiredPointsUp3;
    }
    
    Color normalColor = new(75f / 255f, 132f / 255f, 161f / 255f);
    Color normalSelectedColor = new(58f / 255f, 203f / 255f, 226f / 255f);
    Color lackColor = new(141f / 255f, 45f/255f, 44f/255f);
    Color lackSelectedColor = new(167f / 255f,0,0);
    Color maxLevelColor = new(141f / 255f, 126f / 255f, 44f/225f);
    Color maxLevelSelectedColor = new(255f / 255f, 255f / 255f, 0);
    Color normalKindColor = new(230f / 255f, 230f / 255f, 230f / 255f);
    Color normalKindColorLevel0 = new(28f / 255f, 28f / 255f, 28f / 255f, 128f/225f);

    public SaveDataManager saveDataManager;

    public ShopUI[] shopUI;
    public GameObject initialFriendObj;
    public TextMeshProUGUI initialFriendText;
    public TextMeshProUGUI initialFriendBuyText;

    // Start is called before the first frame update
    void Start()
    {
        //セーブデータから初期化
        shopUI[0].currentLevel = SaveDataManager.data.playerHPLevel;
        shopUI[1].currentLevel = SaveDataManager.data.playerPowerLevel;
        shopUI[2].currentLevel = SaveDataManager.data.castleHPLevel;
        shopUI[3].currentLevel = SaveDataManager.data.friendDropLevel;
        shopUI[4].currentLevel = SaveDataManager.data.friendStrengthenLevel;

        updateKindText();
        updateBuyText();
    }

    public void OnDestroy()
    {
        SaveDataManager.data.playerHPLevel = shopUI[0].currentLevel;
        SaveDataManager.data.playerPowerLevel = shopUI[1].currentLevel;
        SaveDataManager.data.castleHPLevel = shopUI[2].currentLevel;
        SaveDataManager.data.friendDropLevel = shopUI[3].currentLevel;
        SaveDataManager.data.friendStrengthenLevel = shopUI[4].currentLevel;

        saveDataManager.Save();
    }

    // Update is called once per frame
    void Update()
    {
        updateKindText();
        updateBuyText();
        updateInitialFriend();

        //デバッグ
        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    saveDataManager.ResetSaveData();
        //    //セーブデータから初期化
        //    shopUI[0].currentLevel = SaveDataManager.data.playerHPLevel;
        //    shopUI[1].currentLevel = SaveDataManager.data.playerPowerLevel;
        //    shopUI[2].currentLevel = SaveDataManager.data.castleHPLevel;
        //    shopUI[3].currentLevel = SaveDataManager.data.friendDropLevel;
        //    shopUI[4].currentLevel = SaveDataManager.data.friendStrengthenLevel;
        //}
    }

    private void updateInitialFriend()
    {
        if(EventSystem.current.currentSelectedGameObject != initialFriendObj)
        {
            initialFriendBuyText.color = normalColor;
        }
        else
        {
            initialFriendBuyText.color = normalSelectedColor;
        }
    }


    private void updateBuyText()
    {
        for (int i = 0; i < shopUI.Length; i++)
        {
            changeColor(shopUI[i]);
            changeBuyText(shopUI[i]);
        }
    }

    private void changeBuyText(ShopUI shopUI)
    {
        if(shopUI.currentLevel == 0)
        {
            shopUI.BuyText.text = "強化:" + shopUI.requiredPointsUp1+"pt";
        }
        if(shopUI.currentLevel == 1)
        {
            shopUI.BuyText.text = "強化:" + shopUI.requiredPointsUp2+"pt";
        }
        if (shopUI.currentLevel == 2)
        {
            shopUI.BuyText.text = "強化:" + shopUI.requiredPointsUp3+"pt";
        }
        if (shopUI.currentLevel == 3)
        {
            shopUI.BuyText.text = "さいだいレベル!";
        }
    }

    private void changeColor(ShopUI shopUI)
    {
        GameObject currentSelectedObj = EventSystem.current.currentSelectedGameObject;



        if (currentSelectedObj != shopUI.ButtonObj)
        {
            if (shopUI.currentLevel == 0)
            {
                if (shopUI.requiredPointsUp1 <= SaveDataManager.data.playerPoint)
                {
                    shopUI.BuyText.color = normalColor;
                }
                else
                {
                    shopUI.BuyText.color = lackColor;
                }
            }
            if (shopUI.currentLevel == 1)
            {
                if (shopUI.requiredPointsUp2 <= SaveDataManager.data.playerPoint)
                {
                    shopUI.BuyText.color = normalColor;
                }
                else
                {
                    shopUI.BuyText.color = lackColor;
                }
            }
            if (shopUI.currentLevel == 2)
            {
                if (shopUI.requiredPointsUp3 <= SaveDataManager.data.playerPoint)
                {
                    shopUI.BuyText.color = normalColor;
                }
                else
                {
                    shopUI.BuyText.color = lackColor;
                }
            }
            if(shopUI.currentLevel == 3)
            {
                shopUI.BuyText.color = maxLevelColor;
            }
        }
        else
        {
            if (shopUI.currentLevel == 0)
            {
                if (shopUI.requiredPointsUp1 <= SaveDataManager.data.playerPoint)
                {
                    shopUI.BuyText.color = normalSelectedColor;
                }
                else
                {
                    shopUI.BuyText.color = lackSelectedColor;
                }
            }
            if (shopUI.currentLevel == 1)
            {
                if (shopUI.requiredPointsUp2 <= SaveDataManager.data.playerPoint)
                {
                    shopUI.BuyText.color = normalSelectedColor;
                }
                else
                {
                    shopUI.BuyText.color = lackSelectedColor;
                }
            }
            if (shopUI.currentLevel == 2)
            {
                if (shopUI.requiredPointsUp3 <= SaveDataManager.data.playerPoint)
                {
                    shopUI.BuyText.color = normalSelectedColor;
                }
                else
                {
                    shopUI.BuyText.color = lackSelectedColor;
                }
            }
            if (shopUI.currentLevel == 3)
            {
                shopUI.BuyText.color = maxLevelSelectedColor;
            }
        }
    }

    private void updateKindText()
    {
        for(int i=0;i<shopUI.Length;i++)
        {
            switch (shopUI[i].kind)
            {
                case UIKind.PlayerHP:
                    updateKindTextStr(shopUI[i], "HPアップ Lv");
                    break;
                
                case UIKind.PlayerPower: 
                    updateKindTextStr(shopUI[i], "こうげき力アップ Lv");
                    break;
                
                case UIKind.CastleHP:
                    updateKindTextStr(shopUI[i], "城HPアップ Lv");
                    break;
                
                case UIKind.FriendDrop:
                    updateKindTextStr(shopUI[i], "眷属ドロップ率アップ Lv");
                    break;
                
                case UIKind.FriendStrengthen:
                    updateKindTextStr(shopUI[i], "眷属ステータスアップ Lv");
                    break;
            }
        }
    }
    private void updateKindTextStr(ShopUI shopUI,string str) 
    {
        shopUI.KindText.text = str + shopUI.currentLevel.ToString();
        if (shopUI.currentLevel != 0)
        {
            shopUI.KindText.color = normalKindColor;
        }
        else
        {
            shopUI.KindText.color = normalKindColorLevel0;
        }
    }

    public void ButtonPressed(InputAction.CallbackContext context)
    {
        //performの時だけ
        if (context.performed)
        {
            GameObject pressedButtonObj = EventSystem.current.currentSelectedGameObject;

            if (pressedButtonObj == shopUI[(int)UIKind.PlayerHP].ButtonObj)
            {
                upgrade(ref shopUI[(int)UIKind.PlayerHP]);
            }
            if (pressedButtonObj == shopUI[(int)UIKind.PlayerPower].ButtonObj)
            {
                upgrade(ref shopUI[(int)UIKind.PlayerPower]);
            }
            if (pressedButtonObj == shopUI[(int)UIKind.CastleHP].ButtonObj)
            {
                upgrade(ref shopUI[(int)UIKind.CastleHP]);
            }
            if (pressedButtonObj == shopUI[(int)UIKind.FriendDrop].ButtonObj)
            {
                upgrade(ref shopUI[(int)UIKind.FriendDrop]);
            }
            if (pressedButtonObj == shopUI[(int)UIKind.FriendStrengthen].ButtonObj)
            {
                upgrade(ref shopUI[(int)UIKind.FriendStrengthen]);
            }
        }
    }

    public void upgrade(ref ShopUI shopUI)
    {
        if (shopUI.currentLevel == 0)
        {
            if(shopUI.requiredPointsUp1 <= SaveDataManager.data.playerPoint)
            {
                shopUI.currentLevel++;
                SaveDataManager.data.playerPoint -= shopUI.requiredPointsUp1;
            }
        }
        else if(shopUI.currentLevel == 1)
        {
            if (shopUI.requiredPointsUp2 <= SaveDataManager.data.playerPoint)
            {
                shopUI.currentLevel++;
                SaveDataManager.data.playerPoint -= shopUI.requiredPointsUp2;
            }
        }
        else if(shopUI.currentLevel == 2)
        {
            if (shopUI.requiredPointsUp3 <= SaveDataManager.data.playerPoint)
            {
                shopUI.currentLevel++;
                SaveDataManager.data.playerPoint -= shopUI.requiredPointsUp3;
            }
        }
;
    }
}
