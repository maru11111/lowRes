using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageSE : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound;
    bool flagPlayOnce=false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
