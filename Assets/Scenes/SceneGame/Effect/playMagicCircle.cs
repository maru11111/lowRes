using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMagicCircle : MonoBehaviour
{
    public GameObject prefab;


    public void MagicCirclePlay(Vector2 pos)
    {
        GameObject magicCircle = Instantiate(prefab, pos, Quaternion.identity);

        Destroy(magicCircle, 3f);
    }
}
