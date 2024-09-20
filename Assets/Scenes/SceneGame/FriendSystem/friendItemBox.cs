using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum FriendType
{
    slime,
    shield,
    golem,
    mage,
    dragon,
    notExist
}

public class friendItemBox : MonoBehaviour
{
    public Transform playerTransform;
    public Sprite[] images;
    public GameObject[] prefab;
    public Image buttonImage;
    public pause pauseScript;
    public playMagicCircle magicCircle;
    private ImageType currentImageType;
    public bool isSelected;
    public bool friendIsEnabled;
    public bool existItem;
    private bool friendSummoned=false;
    private Vector2 summonPos;
    public FriendType currentFriendType = FriendType.notExist;
    private GameObject friend;
    public baseEnemy baseEnemyScript;
    public GameObject canvas;
    public InputActionReference submitAction;

    private float spawnPosYslime = -0.5f;
    private float spawnPosYshield = -0.5f;
    private float spawnPosYgolem = -0.5f;
    private float spawnPosYmage = -1.95f;
    private float spawnPosYdragon = -0.5f;

    public enum ImageType
    {
        flame,
        flameSelected,
        dragon,
        dragonSelected,
        dragonEnabled,
        dragonSelectedEnabled,
        shield,
        shieldSelected,
        shieldEnabled,
        shieldSelectedEnabled,
        mage,
        mageSelected,
        mageEnabled,
        mageSelectedEnabled,
        slime,
        slimeSelected,
        slimeEnabled,
        slimeSelectedEnabled,
        golem,
        golemSelected,
        golemEnabled,
        golemSelectedEnabled,
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        friendNullCheck();
        checkButtonFocused();
        setCurrentImage();
        changeImage(currentImageType);
    }

    private void friendNullCheck()
    {
        //眷属が死んでいたら
        if(friendIsEnabled && friendSummoned && friend == null)
        {
            //アイテムボックスを初期化
            existItem = false;
            friendIsEnabled = false;
            friendSummoned = false;
            //hpバー非表示
            canvas.SetActive(false);
        }
    }

    public void deleteFrind()
    {
        if (existItem)
        {
            //アイテムボックスを初期化
            existItem = false;
            friendIsEnabled = false;
            friendSummoned = false;
            //hpバー非表示
            canvas.SetActive(false);
            //生きていたら殺す
            if (friend != null)
            {
                Destroy(friend);
            }
        }
    }

    //ボタンが選択(フォーカス)されているかどうか調べる
    private void checkButtonFocused()
    {
        GameObject selectedObj = EventSystem.current.currentSelectedGameObject;

        if (selectedObj != null)
        {
            Debug.Log("selectObj");
            if(selectedObj == this.gameObject)
            {   
                isSelected = true;
            }
            else
            {
                isSelected= false;
            }
        }
    }

    //ボタンが押された時の処理
    public void buttonPressed()
    {
        //召喚キー
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //まだ眷属が召喚されていないなら
            if (!friendIsEnabled)
            {
                Debug.Log("spaceキー");
                if (existItem)
                {
                    //ポーズ中なら
                    if (pauseScript.isPause)
                    {
                        pauseScript.forwardAudio2.Play();
                        //ポーズ解除
                        pauseScript.pauseOff();
                        //アイテムを使う
                        useItem();
                    }
                    //ポーズ中でない
                    else
                    {
                        pauseScript.forwardAudio2.Play();
                        //そのままアイテムを使う
                        useItem();
                    }
                }
            }
            //すでに召喚されていたら
            else
            {
                pauseScript.failAudio.Play();
            }
        }


        //削除キー
        if (Input.GetKeyDown(KeyCode.V)){
            Debug.Log("vキー");
            if (existItem)
            {
                //ポーズ中なら
                if (pauseScript.isPause)
                {
                    pauseScript.forwardAudio2.Play();
                    Debug.Log("Vかつポーズ中v");
                    //警告画面に遷移
                    pauseScript.prevStateType = pauseScript.currentStateType;
                    pauseScript.currentStateType = pause.StateType.caution;
                    EventSystem.current.SetSelectedGameObject(pauseScript.cautionFirstButton);
                    pauseScript.prevFocusButtonForSE = pauseScript.cautionFirstButton;
                    pauseScript.cautionCanvas.SetActive(true);
                    pauseScript.setDeleteBox(this);
                }
                else
                {
                    pauseScript.backAudio.Play();
                    //そのまま削除
                    deleteFrind();
                }
            }
        }
    }

    public void getFriend(FriendType friendType)
    {
        currentFriendType = friendType;
        existItem = true;
    }

    private void useItem()
    {
        //まだ召喚されていないなら
        if (!friendIsEnabled)
        {
            //出現位置を決定
            setSummonPos();
            //魔法陣作成
            magicCircle.MagicCirclePlay(summonPos);

            friendIsEnabled = true;
            //魔法陣が出てから召喚
            StartCoroutine(delaySummonForMagicCircle());
        }
    }

    private IEnumerator delaySummonForMagicCircle()
    {
        //魔法陣が出るまで待つ
        yield return new WaitForSeconds(1.2f);

        if (currentFriendType != FriendType.notExist)
        {
            friend = Instantiate(prefab[(int)currentFriendType], summonPos, Quaternion.identity);
        }
        baseEnemyScript = friend.GetComponent<baseEnemy>();
        baseEnemyScript.isEnemy = false;
        friendSummoned = true;
        //hpバー表示
        canvas.SetActive(true);
    }


    private void setSummonPos()
    {
        switch (currentFriendType)
        {
            case FriendType.slime:
                summonPos = new Vector2(playerTransform.position.x, spawnPosYslime);
                break;
            case FriendType.shield:
                summonPos = new Vector2(playerTransform.position.x, spawnPosYshield);
                break;
            case FriendType.golem:
                summonPos = new Vector2(playerTransform.position.x, spawnPosYgolem);
                break;
            case FriendType.mage:
                summonPos = new Vector2(playerTransform.position.x, spawnPosYmage);
                break;
            case FriendType.dragon:
                summonPos = new Vector2(playerTransform.position.x, spawnPosYdragon);
                break;
        }
    }

    public void setCurrentImage()
    {
        if (!existItem)
        {
            if (!isSelected)
            {
                currentImageType = ImageType.flame;
            }
            else
            {
                currentImageType = ImageType.flameSelected;
            }
        }
        else
        {
            switch(currentFriendType)
            {
                case FriendType.slime:
                    selectSlimeFlame();
                    break;

                case FriendType.shield:
                    selectShieldFlame();
                    break;

                case FriendType.golem:
                    selectGolemFlame();
                    break;

                case FriendType.mage:
                    selectMageFlame();
                    break;

                case FriendType.dragon:
                    selectDragonFlame();
                    break;
            }
        }
    }

    private void selectSlimeFlame()
    {
        if (isSelected)
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.slimeSelectedEnabled;
            }
            else
            {
                currentImageType = ImageType.slimeSelected;
            }
        }
        else
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.slimeEnabled;
            }
            else
            {
                currentImageType = ImageType.slime;
            }
        }
    }

    private void selectShieldFlame()
    {
        if (isSelected)
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.shieldSelectedEnabled;
            }
            else
            {
                currentImageType = ImageType.shieldSelected;
            }
        }
        else
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.shieldEnabled;
            }
            else
            {
                currentImageType = ImageType.shield;
            }
        }
    }

    private void selectGolemFlame()
    {
        if (isSelected)
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.golemSelectedEnabled;
            }
            else
            {
                currentImageType = ImageType.golemSelected;
            }
        }
        else
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.golemEnabled;
            }
            else
            {
                currentImageType = ImageType.golem;
            }
        }
    }

    private void selectMageFlame()
    {
        if (isSelected)
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.mageSelectedEnabled;
            }
            else
            {
                currentImageType = ImageType.mageSelected;
            }
        }
        else
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.mageEnabled;
            }
            else
            {
                currentImageType = ImageType.mage;
            }
        }
    }

    private void selectDragonFlame()
    {
        if (isSelected)
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.dragonSelectedEnabled;
            }
            else
            {
                currentImageType = ImageType.dragonSelected;
            }
        }
        else
        {
            if (friendIsEnabled)
            {
                currentImageType = ImageType.dragonEnabled;
            }
            else
            {
                currentImageType = ImageType.dragon;
            }
        }
    }


    public void changeImage(ImageType type)
    {
        buttonImage.sprite = images[(int)type];
    }
}
