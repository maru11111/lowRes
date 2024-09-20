using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GetActionManager : MonoBehaviour
{
    public GameObject getActObj;
    public TextMeshProUGUI getActText;
    public SaveDataManager saveDataManager;
    public AudioSource getActSE;
 
    private bool flag = false;
    private bool flag2 = false;
    private float timer = 0;
    private float timerTime = 1.2f;

    public GameObject Stage1Obj;

    // Start is called before the first frame update
    void Start()
    {
        //テキストを変更
        if (SaveDataManager.data.isStage1PerfectClear == 1 && SaveDataManager.data.isStage1PerfectClearFirstFlag == 0)
        {
            getActText.text = "<color=#ffff00>【Sランク報酬】</color>\n二段ジャンプが使えるようになった！\n<color=#a3a3a3></color>";
            
            SaveDataManager.data.isStage1PerfectClearFirstFlag = 1;
            SaveDataManager.data.onDoubleJump = 1;
            saveDataManager.Save();
        }
        if(SaveDataManager.data.isStage2PerfectClear == 1 && SaveDataManager.data.isStage2PerfectClearFirstFlag == 0)
        {
            getActText.text = "<color=#ffff00>【Sランク報酬】</color>\nローリングが使えるようになった！\n<color=#a3a3a3>(Qキーで使用できます)</color>";

            SaveDataManager.data.onRolling = 1;
            SaveDataManager.data.isStage2PerfectClearFirstFlag = 1;
            saveDataManager.Save();
        }
        if (SaveDataManager.data.isStage3PerfectClear == 1 && SaveDataManager.data.isStage3PerfectClearFirstFlag == 0)
        {
            getActText.text = "<color=#ffff00>【Sランク報酬】</color>\n世田谷祭までに実装予定\n<color=#a3a3a3>(Qキー)</color>";

            SaveDataManager.data.isStage3PerfectClearFirstFlag = 1;
            saveDataManager.Save();
        }
        getActSE.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!flag)
        {
            getActObj.transform.DOLocalMoveX(-1280, 1.2f).SetEase(Ease.InOutQuint);
            flag = true;
        }
        if (flag)
        {
            timer += Time.deltaTime;
        }

        if(timerTime <= timer)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !flag2)
            {

                getActObj.transform.DOLocalMoveX(-1280 * 2 - 10, 1f).SetEase(Ease.OutQuint);
                EventSystem.current.SetSelectedGameObject(Stage1Obj);
                flag2 = false;
            }
        }
    }
}
