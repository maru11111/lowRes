using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class pause : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputActionAsset actionAsset;
    public InputSystemUIInputModule inputModule;
    public bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void pauseOn()
    {
        Time.timeScale = 0f;
        isPause = true;
        //アクションマップを変更
        playerInput.SwitchCurrentActionMap("UI");
        //InputSystemUIInputModuleを変更
        var actionMap = actionAsset.FindActionMap("UI");
        inputModule.move = InputActionReference.Create(actionMap.FindAction("Navigate"));
        inputModule.submit = InputActionReference.Create(actionMap.FindAction("Submit"));
        inputModule.cancel = InputActionReference.Create(actionMap.FindAction("Cancel"));
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
        Time.timeScale = 1f;
    }
}
