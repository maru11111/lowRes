using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitStop : MonoBehaviour
{
    public static hitStop hitStopInstance;

    // Start is called before the first frame update
    private void Start()
    {
        hitStopInstance = this;
    }

    public void startHitStop(float time)
    {
        hitStopInstance.StartCoroutine(hitStopInstance.hitStopCoroutine(time));
    }

    private IEnumerator hitStopCoroutine(float time)
    {
        //時間を止める
        Time.timeScale = 0f;

        //timeだけ待つ
        yield return new WaitForSecondsRealtime(time);

        //時間を戻す
        Time.timeScale = 1f;
    }

}
