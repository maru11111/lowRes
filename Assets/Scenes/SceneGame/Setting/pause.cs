using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputActionAsset actionAsset;
    public InputSystemUIInputModule inputModule;
    public bool isPause;
    public GameObject playerUICanvas;
    public GameObject ItemBoxCanvas;
    public GameObject PauseCanvas;
    public GameObject ItemBoxCanvasForStage;
    public AudioSource BGM;
    private float prevVolume;
    public GameObject pauseFirstButton;
    public GameObject friendFirstButton;
    public GameObject cautionFirstButton;
    public GameObject prevButton;
    public StateType currentStateType;
    public StateType prevStateType;
    public GameObject cautionCanvas;
    public friendItemBox deleteBoxScript;
    public AudioSource forwardAudio1;
    public AudioSource forwardAudio2;
    public AudioSource backAudio;
    public AudioSource focusAudio;
    public AudioSource failAudio;
    public GameObject currentFocusButtonForSE;
    public GameObject prevFocusButtonForSE;

    public enum StateType
    {
        pause,
        friendSelect,
        surrender,
        caution
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CurrentStateType: " + currentStateType);
    }

    public void pauseOn()
    {
        if (!isPause)
        {
            //Debug.Log("pauseOn called by: " + EventSystem.current.currentSelectedGameObject);
            Time.timeScale = 0;
            isPause = true;
            //ポーズ音を鳴らす
            forwardAudio1.Play();
            //アクションマップを変更
            playerInput.SwitchCurrentActionMap("UI");
            //InputSystemUIInputModuleを変更
            var actionMap = actionAsset.FindActionMap("UI");
            inputModule.move = InputActionReference.Create(actionMap.FindAction("Navigate"));
            inputModule.submit = InputActionReference.Create(actionMap.FindAction("Submit"));
            inputModule.cancel = InputActionReference.Create(actionMap.FindAction("Cancel"));
            //キャンバスのオンオフ
            playerUICanvas.SetActive(false);
            ItemBoxCanvas.GetComponent<Canvas>().enabled = false;
            PauseCanvas.SetActive(true);
            //BGM小さく
            prevVolume = BGM.volume;
            Debug.Log("preb:" + prevVolume);
            BGM.volume = 0.3f;
            //UIの初期選択を変更
            prevButton = EventSystem.current.currentSelectedGameObject;
            //オブジェクトを無効にするとコルーチンが破棄されてしまうためキャンバスをオフにしている。ちなみにこっちの方が高速らしい
            prevFocusButtonForSE = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            //現在のステートタイプを更新
            currentStateType = StateType.pause;
        }
    }

    public void pauseOff()
    {
        //アクションマップを変更
        playerInput.SwitchCurrentActionMap("Action");
        //InputSystemUIInputModuleを変更
        var actionMap = actionAsset.FindActionMap("Action");
        inputModule.move = InputActionReference.Create(actionMap.FindAction("Navigate"));
        inputModule.submit = InputActionReference.Create(actionMap.FindAction("Submit"));
        inputModule.cancel = null;
        isPause = false;
        //キャンバスのオンオフ
        playerUICanvas.SetActive(true);
        //
        ItemBoxCanvas.GetComponent <Canvas>().enabled = true;
        PauseCanvas.SetActive(false);
        //BGMを戻す
        BGM.volume = prevVolume;
        Time.timeScale = 1f;
        //UIの初期選択を変更
        EventSystem.current.SetSelectedGameObject(friendFirstButton);
        prevFocusButtonForSE = friendFirstButton;
    }

    ////決定キーを押したとき
    //public void nextState()
    //{
    //    switch (currentStateType)
    //    {
    //        case StateType.pause:
    //            break;
    //        case StateType.friendSelect:
    //            break;
    //        case StateType.surrender:
    //            break;
    //        case StateType.caution:
    //            break;
    //    }
    //}

    //キャンセルキーまたは戻るボタンを押したとき
    public void prevStateForPlayerInput(InputAction.CallbackContext context)
    {
        //3回のコールバックのうちperformedのときだけ動かす
        if (context.performed)
        {
            prevState();
        }
    }

    public void prevStateForButton()
    {
        prevState();
    }

    private void prevState()
    {
        backAudio.Play();
        switch (currentStateType)
        {
            case StateType.pause:
                //ポーズ画面を閉じる
                Debug.Log("ポーズ画面を閉じました");
                pauseOff();
                break;

            case StateType.friendSelect:
                //眷属画面からポーズ画面に戻る
                Debug.Log("眷属画面からポーズ画面に戻りました");
                ItemBoxCanvas.GetComponent<Canvas>().enabled = false;
                ItemBoxCanvasForStage.SetActive(true);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
                prevFocusButtonForSE = pauseFirstButton;
                currentStateType = StateType.pause;
                break;

            case StateType.surrender:
                //降伏画面からポーズ画面に戻る
                cautionCanvas.SetActive(false);
                EventSystem.current.SetSelectedGameObject(prevButton);
                prevFocusButtonForSE = prevButton;
                currentStateType = StateType.pause;
                break;

            case StateType.caution:
                //前の画面が眷属画面なら
                if (prevStateType == StateType.friendSelect)
                {
                    cautionToFriend();
                }
                break;
        }
    }

    public void cautionToFriend()
    {
        //注意画面から眷属画面に戻る
        cautionCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(prevButton);
        prevFocusButtonForSE = prevButton;
        currentStateType = StateType.friendSelect;
    }

    public void setDeleteBox(friendItemBox friendItemBoxScript)
    {
        deleteBoxScript = friendItemBoxScript;
    }

    public void OnCautionButton()
    {
        if (currentStateType == StateType.friendSelect)
        {
            forwardAudio2.Play();
            deleteBoxScript.deleteFrind();
            cautionToFriend();
        }
        else if (currentStateType == StateType.surrender)
        {
            forwardAudio2.Play();
            //タイムスケールを元に戻す
            Time.timeScale = 1;
            SceneManager.LoadScene("SceneHome");
        }

    }

    public void focusSE(InputAction.CallbackContext context)
    {
        //canceledの方がなぜか先に呼ばれている...?

        //3回のコールバックのうちperformedのとき
        if (context.canceled)
        {
            Debug.Log("Performed:" + EventSystem.current.currentSelectedGameObject);
            if (prevFocusButtonForSE != EventSystem.current.currentSelectedGameObject)
            {
                focusAudio.Play();
            }
                    }

        //3回のコールバックのうちcanceledのとき
        if (context.performed)
        {
            Debug.Log("Canceled:" + EventSystem.current.currentSelectedGameObject);
            prevFocusButtonForSE = EventSystem.current.currentSelectedGameObject;

        }
    }
}
