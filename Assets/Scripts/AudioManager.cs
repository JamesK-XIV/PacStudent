using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        aud.clip = clips[0];
        aud.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(aud.isPlaying))
        {
            aud.clip = clips[1];
            aud.Play();
        }
        
    }
}