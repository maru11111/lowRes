using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castleBrokenEffectPlay : MonoBehaviour
{
    public castleBrokenEffect castleBrokenEffect;

    public void play(Vector2 pos)
    {
        Instantiate(castleBrokenEffect, pos, Quaternion.identity);
    }
}