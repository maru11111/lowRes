using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class guardSE : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sound;
    bool flagPlayOnce = false;

    // Update is called once per frame
    void Update()
    {
        if (!flagPlayOnce)
        {
            audioSource.PlayOneShot(sound);
            flagPlayOnce = true;

        }
    }
}
