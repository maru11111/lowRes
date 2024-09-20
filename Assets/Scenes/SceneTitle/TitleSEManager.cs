using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TitleSEManager : MonoBehaviour
{
    public AudioSource forwardAudio;
    public AudioSource focusAudio;
    public AudioSource backAudio;

    private GameObject prevFocusObj;
    public GameObject yesObj;
    public GameObject noObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //決定キーを押したときの音
    public void submitButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(EventSystem.current.currentSelectedGameObject != noObj)
            {
                forwardAudio.Play();
            }
            else
            {
                backAudio.Play();
            }
        }
    }
    //キャンセルーを押したときの音
    public void cancelButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(EventSystem.current.currentSelectedGameObject == noObj||EventSystem.current.currentSelectedGameObject == yesObj)
            {
                backAudio.Play();
            }
        }
    }

        public void focusSE(InputAction.CallbackContext context)
    {
        //canceledの方がなぜか先に呼ばれている...?

        //3回のコールバックのうちperformedのとき
        if (context.canceled)
        {
            if (prevFocusObj != EventSystem.current.currentSelectedGameObject)
            {
                focusAudio.Play();
            }
        }

        //3回のコールバックのうちcanceledのとき
        if (context.performed)
        {
            prevFocusObj = EventSystem.current.currentSelectedGameObject;

        }
    }
}
