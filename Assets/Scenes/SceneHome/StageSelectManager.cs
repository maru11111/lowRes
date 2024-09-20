using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    [System.Serializable]
    public struct Stage
    {
        public string name;
        public GameObject StageObj;
        public TextMeshProUGUI stageText;
        public Button stageButton;
    }

    public Stage[] stages;
    public GameObject blackRectObj;
    public GameObject commonStageButton;

    public TextMeshProUGUI rankText;
    public TextMeshProUGUI scoreText;

    private GameObject currentSelectedObj;

    // Start is called before the first frame update
    void Start()
    {
        changeScoreText(SaveDataManager.data.stage1HighScore);

        if (CommonParam.SRankBorder1 < SaveDataManager.data.stage1HighScore)
        {
            rankText.text = "S";
            rankText.color = new Color(255f / 255f, 212f / 255f, 98f / 255f);
        }
        else if (CommonParam.ARankBorder1 < SaveDataManager.data.stage1HighScore)
        {
            rankText.text = "A";
            rankText.color = new Color(241f / 255f, 54f / 255f, 53f / 255f);
        }
        else if (CommonParam.BRankBorder1 < SaveDataManager.data.stage1HighScore)
        {
            rankText.text = "B";
            rankText.color = new Color(60f / 255f, 95f / 255f, 255f / 255f);
        }
        else if (SaveDataManager.data.stage1HighScore != 0)
        {
            rankText.text = "C";
            rankText.color = new Color(37f / 255f, 224f / 225f, 87f / 225f);
        }
        else
        {
            rankText.text = "-";
            rankText.color = new Color(230f / 255f, 230f / 225f, 230f / 225f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeStageInteractive();
    }

    public void changeRankTexts()
    {
        currentSelectedObj = EventSystem.current.currentSelectedGameObject;

        if (currentSelectedObj == stages[0].StageObj /*|| currentSelectedObj == commonStageButton*/)
        {
            changeScoreText(SaveDataManager.data.stage1HighScore);
            changeRankText(SaveDataManager.data.stage1HighScore, currentSelectedObj);
        }
        if (currentSelectedObj == stages[1].StageObj)
        {
            changeScoreText(SaveDataManager.data.stage2HighScore);
            changeRankText(SaveDataManager.data.stage2HighScore, currentSelectedObj);
        }
        if (currentSelectedObj == stages[2].StageObj)
        {
            changeScoreText(SaveDataManager.data.stage3HighScore);
            changeRankText(SaveDataManager.data.stage3HighScore, currentSelectedObj);
        }
    }

    private void changeStageInteractive()
    {
        if (SaveDataManager.data.isStage1Clear == 1) stages[1].stageButton.interactable = true;
        if (SaveDataManager.data.isStage2Clear == 1) stages[2].stageButton.interactable = true;
    }

    private void changeScoreText(int highScore)
    {
        if (SaveDataManager.data.stage1HighScore != 0)
        {
            scoreText.text = highScore.ToString();
        }
        else
        {
            scoreText.text = "-";
        }
    }

    private void changeRankText(int highScore, GameObject selectObj)
    {
        if (selectObj == stages[0].StageObj /*|| selectObj == commonStageButton*/)
        {
            if (CommonParam.SRankBorder1 < highScore)
            {
                rankText.text = "S";
                rankText.color = new Color(255f / 255f, 212f / 255f, 98f / 255f);
            }
            else if (CommonParam.ARankBorder1 < highScore)
            {
                rankText.text = "A";
                rankText.color = new Color(241f / 255f, 54f / 255f, 53f / 255f);
            }
            else if (CommonParam.BRankBorder1 < highScore)
            {
                rankText.text = "B";
                rankText.color = new Color(60f / 255f, 95f / 255f, 255f / 255f);
            }
            else if (highScore != 0)
            {
                rankText.text = "C";
                rankText.color = new Color(37f / 255f, 224f / 225f, 87f / 225f);
            }
            else
            {
                rankText.text = "-";
                rankText.color = new Color(230f / 255f, 230f / 225f, 230f / 225f);
            }
        }
        if (selectObj == stages[1].StageObj)
        {
            if (CommonParam.SRankBorder2 < highScore)
            {
                rankText.text = "S";
                rankText.color = new Color(255f / 255f, 212f / 255f, 98f / 255f);
            }
            else if (CommonParam.ARankBorder2 < highScore)
            {
                rankText.text = "A";
                rankText.color = new Color(241f / 255f, 54f / 255f, 53f / 255f);
            }
            else if (CommonParam.BRankBorder2 < highScore)
            {
                rankText.text = "B";
                rankText.color = new Color(60f / 255f, 95f / 255f, 255f / 255f);
            }
            else if (highScore != 0)
            {
                rankText.text = "C";
                rankText.color = new Color(37f / 255f, 224f / 225f, 87f / 225f);
            }
            else
            {
                rankText.text = "-";
                rankText.color = new Color(230f / 255f, 230f / 225f, 230f / 225f);
            }
        }
        if (selectObj == stages[2].StageObj || selectObj == commonStageButton)
        {
            if (CommonParam.SRankBorder3 < highScore)
            {
                rankText.text = "S";
                rankText.color = new Color(255f / 255f, 212f / 255f, 98f / 255f);
            }
            else if (CommonParam.ARankBorder3 < highScore)
            {
                rankText.text = "A";
                rankText.color = new Color(241f / 255f, 54f / 255f, 53f / 255f);
            }
            else if (CommonParam.BRankBorder3 < highScore)
            {
                rankText.text = "B";
                rankText.color = new Color(60f / 255f, 95f / 255f, 255f / 255f);
            }
            else if (highScore != 0)
            {
                rankText.text = "C";
                rankText.color = new Color(37f / 255f, 224f / 225f, 87f / 225f);
            }
            else
            {
                rankText.text = "-";
                rankText.color = new Color(230f / 255f, 230f / 225f, 230f / 225f);
            }
        }
    }

    public void buttonPressed(InputAction.CallbackContext context)
    {
        currentSelectedObj = EventSystem.current.currentSelectedGameObject;
        
        if (context.performed)
        {
            //ステージのレベルを設定
            if (currentSelectedObj == stages[0].StageObj)
            {
                CommonParam.selectStageLevel = CommonParam.StageLevel.Level1;
                //シーン遷移
                StartCoroutine(changeScene());
            }
            if (currentSelectedObj == stages[1].StageObj)
            {
                CommonParam.selectStageLevel = CommonParam.StageLevel.Level2;
                //シーン遷移
                StartCoroutine(changeScene());
            }
            if (currentSelectedObj == stages[2].StageObj)
            {
                CommonParam.selectStageLevel = CommonParam.StageLevel.Level3;
                //シーン遷移
                StartCoroutine(changeScene());
            }
        }
    }

    private IEnumerator changeScene()
    {

        blackRectObj.transform.DOMoveX(1280 / 2, 0.25f);

        //シーン遷移アニメーションが終わるまで待つ
        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene("SceneGame");

        //SceneManager.UnloadSceneAsync("SceneHome");

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SceneGame");;


        //while(!asyncLoad.isDone)
        //{
        //    yield return null;
        //}

    }
}
