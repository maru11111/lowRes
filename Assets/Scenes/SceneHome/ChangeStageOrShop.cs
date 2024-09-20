using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeStageOrShop : MonoBehaviour
{
    public GameObject stageSelectCanvas;
    public GameObject shopCanvas;
    public GameObject getActCanvas;

    public TextMeshProUGUI stageText;
    public TextMeshProUGUI shopText;

    public GameObject stageButton;
    public GameObject shopButton;

    public TextMeshProUGUI pointsText;
    public SaveDataManager saveDataManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == stageButton)
        {
          
            stageSelectCanvas.SetActive(true);
            shopCanvas.SetActive(false);
        }
        if (EventSystem.current.currentSelectedGameObject == shopButton)
        {
            stageSelectCanvas.SetActive(false);
            shopCanvas.SetActive(true);
        }
        pointsText.text = "所持ポイント:" + SaveDataManager.data.playerPoint + "pt";

        
        if (EventSystem.current.currentSelectedGameObject == stageButton)
        {
            stageText.color = new Color(230f / 225f, 230f / 225f, 230f / 225f);
            shopText.color = new Color(92f / 225f, 92f / 225f, 92f / 225f);
        }
        else if (stageSelectCanvas.activeSelf)
        {
            shopText.color = new Color(92f / 225f, 92f / 225f, 92f / 225f);
            stageText.color = new Color(141f / 225f, 141f / 225f, 141f / 225f);
        }
        else if (EventSystem.current.currentSelectedGameObject == shopButton)
        {
            shopText.color = new Color(230f / 225f, 230f / 225f, 230f / 225f);
            stageText.color = new Color(92f / 225f, 92f / 225f, 92f / 225f);
        }
        else if (shopCanvas.activeSelf)
        {
            stageText.color = new Color(92f / 225f, 92f / 225f, 92f / 225f);
            shopText.color = new Color(141f / 225f, 141f / 225f, 141f / 225f);
        }

        if(SaveDataManager.data.isStage1PerfectClear == 1 && SaveDataManager.data.isStage1PerfectClearFirstFlag == 0)
        {
            //Sランク報酬のキャンバスを表示
            getActCanvas.SetActive(true);
            //選択しているボタンをなくす
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (SaveDataManager.data.isStage2PerfectClear == 1 && SaveDataManager.data.isStage2PerfectClearFirstFlag == 0)
        {
            //Sランク報酬のキャンバスを表示
            getActCanvas.SetActive(true);
            //選択しているボタンをなくす
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (SaveDataManager.data.isStage3PerfectClear == 1 && SaveDataManager.data.isStage3PerfectClearFirstFlag == 0)
        {
            //Sランク報酬のキャンバスを表示
            getActCanvas.SetActive(true);
            //選択しているボタンをなくす
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
