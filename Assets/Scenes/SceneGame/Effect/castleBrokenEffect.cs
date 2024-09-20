using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castleBrokenEffect : MonoBehaviour
{
    public damageEffect DamageEffect;

    public float timer=0;
    private float interval=1f;
    private Vector2 pos;
    private float range=1.5f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (interval <= timer)
        {
            pos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(Random.Range(-range, range), Random.Range(-range, range));
            DamageEffect.damageEffectPlay(pos, 0.05f);
            timer -= interval;
        }
    }
}