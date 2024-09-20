using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CautionManager : MonoBehaviour
{
    public GameObject cautionCanvas;

    public TextMeshProUGUI titleText;

    public SaveDataManager saveDataManager;

    public GameObject yesObj;
    public GameObject noObj;
    public GameObject startObj;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startOn()
    {
        SceneManager.LoadScene("SceneHome");
    }

    public void cautionOn()
    {
        cautionCanvas.SetActive(true);
        titleText.color = new Color(100f/255f, 100f / 255f, 100f / 255f);
        EventSystem.current.SetSelectedGameObject(noObj);
    }
    public void cautionOff()
    {
        cautionCanvas.SetActive(false);
        titleText.color = new Color(1, 1, 1);
        EventSystem.current.SetSelectedGameObject(startObj);
    }

    public void yesPressed()
    {
        cautionOff();
        saveDataManager.ResetSaveData();
    }
    public void noPressed()
    {
        cautionOff();
    }
}
