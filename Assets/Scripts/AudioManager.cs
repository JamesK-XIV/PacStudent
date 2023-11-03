using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip[] clips;
    public GameConnector connector;
    public bool recover = false;
    // Start is called before the first frame update
    void Start()
    {
        connector = GameObject.FindGameObjectWithTag("Connector").GetComponent<GameConnector>();
        aud.clip = clips[0];
        aud.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aud.isPlaying)
        {
            if (connector.GhostManager.getMusic() == GhostManager.GhostsMusic.scared && GameConnector.currentGameState == GameConnector.GameState.Start)
            {
                aud.clip = clips[2];
            }
            else
            {
                aud.clip = clips[1];
            }
            aud.Play();
        }

    }
    public void scaredGhosts()
    {
        aud.clip = clips[2];
        aud.Play();
    }
    public void ghostRecover()
    {
        aud.Stop();
        if (!recover)
        {
            StartCoroutine(recoverSong());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(recoverSong());
        }
    }
    IEnumerator recoverSong()
    {
        recover = true;
        aud.clip = clips[3];
        aud.Play();
        yield return new WaitForSeconds(1.8f);
        aud.Stop();
        recover = false;
    }
    public void playerDeath()
    {
        aud.Stop();
        aud.clip = clips[4];
        aud.Play();
    }
    public void stopMusic()
    {
        aud.Stop();
    }
}
