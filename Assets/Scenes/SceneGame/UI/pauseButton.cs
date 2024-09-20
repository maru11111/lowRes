using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class pauseButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject itemBoxCanvas;
    public GameObject cautionCanvas;
    public GameObject friendFirstButton;
    public GameObject cautionFirstButton;
    public GameObject itemBoxCanvasForStage;
    public pause pauseScript;
    public GameObject blackRectForAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void checkButtonFocused()
    {
        GameObject selectedObj = EventSystem.current.currentSelectedGameObject;

        if (selectedObj != null)
        {
            if (selectedObj == this.gameObject)
            {
                text.color = new Color(250f / 255f, 250f / 255f, 250f / 255f);
            }
            else
            {
                text.color = new Color(86f / 255f, 86f / 255f, 86f / 255f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkButtonFocused();   
    }

    public void friendButton()
    {
        //音を鳴らす
        pauseScript.forwardAudio2.Play();
        //眷属画面に移動する
        //眷属の選択UIを表示
        itemBoxCanvas.GetComponent<Canvas>().enabled = true;
        itemBoxCanvasForStage.SetActive(false);
        //UIの初期選択を変更
        EventSystem.current.SetSelectedGameObject(friendFirstButton);
        pauseScript.prevFocusButtonForSE = friendFirstButton;
        //現在のステートタイプを更新
        pauseScript.currentStateType = pause.StateType.friendSelect;
    }

    //撤退ボタンを押したときの処理
    public void surrenderButton()
    {
        //警告を表示
        //音を鳴らす
        pauseScript.forwardAudio2.Play();
        //警告のキャンバスオン
        cautionCanvas.SetActive(true);
        //直前まで選択していたボタンを保存
        pauseScript.prevButton = EventSystem.current.currentSelectedGameObject;
        //初期選択ボタンをセット
        EventSystem.current.SetSelectedGameObject(cautionFirstButton);
        //SE用直前まで選択していたボタンを現在のボタンに変更(SEが誤ってならないようにするため)
        pauseScript.prevFocusButtonForSE = cautionFirstButton;
        //現在のUIステートを更新
        pauseScript.currentStateType = pause.StateType.surrender;
    }
}
