using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class cautionButton : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI text;
    public pause pauseScript;

    private void checkButtonFocused()
    {
        GameObject selectedObj = EventSystem.current.currentSelectedGameObject;

        if (selectedObj != null)
        {
            if (selectedObj == this.gameObject)
            {
                text.color = new Color(1,1,1);
            }
            else
            {
                text.color = new Color(115f / 255f, 115f / 255f, 115f / 255f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkButtonFocused();
        if(pauseScript.currentStateType == pause.StateType.surrender)
        {
            messageText.text = "本当に撤退しますか？";
        }
        if(pauseScript.currentStateType == pause.StateType.caution)
        {
            messageText.text = "本当に捨てますか？";
        }
    }
}
