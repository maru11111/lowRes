using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageSE : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sound;
    bool flagPlayOnce=false;

    // Start is called before the first frame update
    void Start()
    {
    }

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
