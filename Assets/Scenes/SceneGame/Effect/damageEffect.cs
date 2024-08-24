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

        Destroy(explosion, 1f);
    }
}
