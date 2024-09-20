using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class damageEffect : MonoBehaviour
{
    public GameObject prefab;
    public void damageEffectPlay(Vector2 pos)
    {
        GameObject explosion = Instantiate(prefab, pos, Quaternion.identity);

        Destroy(explosion, 0.5f);
    }

    public void damageEffectPlay(Vector2 pos, float volume)
    {
        GameObject explosion = Instantiate(prefab, pos, Quaternion.identity);

        explosion.transform.Find("DamageSE").GetComponent<damageSE>().audioSource.volume = volume;

        Destroy(explosion, 0.5f);
    }
}
