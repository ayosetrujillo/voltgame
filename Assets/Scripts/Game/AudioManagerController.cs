using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerController : MonoBehaviour
{
    public static AudioManagerController instance;
    public AudioSource titleMusic, mainMusic, bossMusic, finalTheme;
    public AudioSource[] soundFXList;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayTitleTheme()
    {
        titleMusic.Play();
        mainMusic.Stop();
        bossMusic.Stop();
        finalTheme.Stop();
    }

    public void PlayMainTheme()
    {
        titleMusic.Stop();
        mainMusic.Play();
        bossMusic.Stop();
        finalTheme.Stop();
    }

    public void PlayBossTheme()
    {
        titleMusic.Stop();
        mainMusic.Stop();
        bossMusic.Play();
        finalTheme.Stop();
    }

    public void PlayFinalTheme()
    {
        titleMusic.Stop();
        mainMusic.Stop();
        bossMusic.Stop();
        finalTheme.Play();

    }



    public void StopAllMusic()
    {
        titleMusic.Stop();
        mainMusic.Stop();
        bossMusic.Stop();
        finalTheme.Stop();
    }

    // SOUND FX PLAYER

    public void PlaySFX(int soundPosition)
    {
        soundFXList[soundPosition].Stop();
        soundFXList[soundPosition].Play();
        // soundFXList[soundPosition].PlayOneShot(soundFXList[soundPosition].clip);

    }


    public void PlaySFXPitch(int soundPosition)
    {
        soundFXList[soundPosition].Stop();
        soundFXList[soundPosition].pitch = Random.Range(0.8f, 1.2f);
        soundFXList[soundPosition].Play();
    }

    public void StopSFX( int soundPosition)
    {
        soundFXList[soundPosition].Stop();
    }


}
