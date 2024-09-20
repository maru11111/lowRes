using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class openningAnimation : MonoBehaviour
{    
    private void Awake()
    {
        Time.timeScale = 0;
        StartCoroutine(playAnimation());

    }

    IEnumerator playAnimation()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        this.transform.DOMoveX(-1280 / 2, 0.25f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1;
    }

}
