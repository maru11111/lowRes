using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using TMPro;

public class changeResultScene : MonoBehaviour
{
    public judgeGameResult judgeGameResult;
    public GameObject resultCanvas;
    public GameObject playerUICanvas;
    public GameObject itemCanvas;

    public GameObject mainCamera;
    public GameObject playerCastle;
    public GameObject enemyCastle;

    public Collider2D playerCollider;
    public player playerScript;
    public PlayerInput playerInput;

    public castleBrokenEffectPlay castleBrokenEffect;
    
    public AudioSource stageBGM;

    public AudioSource clearSE;
    public AudioSource failSE;

    private bool flagOnce=false;

    public textCountDownTime textCountDownTimeScript;

    //後で治す
    private IEnumerator forPlayerIsDead()
    {
        if (playerScript.isDead)
        {
            textCountDownTimeScript.isWaitEnd = true;
            yield return null;
        }
        if (!flagOnce)
        {

            //キャンバスのオンオフ
            resultCanvas.SetActive(true);
            playerUICanvas.SetActive(false);
            itemCanvas.SetActive(false);
            //SE
            if (judgeGameResult.isPlayerWin)
            {
                clearSE.Play();
            }
            else
            {
                failSE.Play();
            }
            //敵城を壊していたら
            if (judgeGameResult.isBreakCastle)
            {
                //カメラ移動
                mainCamera.transform.DOMoveX(enemyCastle.transform.position.x, 1f);
                //mainCamera.transform.DOMoveY(enemyCastle.transform.position.y, 1f);
                //城破壊エフェクト
                castleBrokenEffect.play(enemyCastle.transform.position);
            }
            //プレイヤー城が壊されていたら
            if (!judgeGameResult.isDefendCastle)
            {
                //カメラ移動
                mainCamera.transform.DOMoveX(playerCastle.transform.position.x, 1f);
                //mainCamera.transform.DOMoveY(playerCastle.transform.position.y, 1f);
                //城破壊エフェクト
                castleBrokenEffect.play(playerCastle.transform.position);
            }
            //プレイヤーを動けなくする
            playerScript.isGameSet = true;
            //ローリングモーションをしているとバグが起きるので待機にする
            playerScript.anim.Play("Idle");
            //プレイヤーの当たり判定をオフ
            playerCollider.enabled = false;
            //アクションマップを変更
            playerInput.SwitchCurrentActionMap("NoAct");
            //ステージBGMを停止
            stageBGM.Stop();

            flagOnce = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (judgeGameResult.isJudgeEnd)
        {
            StartCoroutine(forPlayerIsDead());
        }
    }
}
