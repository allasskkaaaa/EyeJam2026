using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;


    public void changeSong(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.PlayOneShot(clip);
    }

    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void pauseMusic(float seconds)
    {
        StartCoroutine(PauseMusic(seconds)); 
    }

    IEnumerator PauseMusic(float seconds)
    {
        musicSource.Pause();
        yield return new WaitForSeconds(seconds);
        musicSource.UnPause();
    }
}
