using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;

    public float masterVolume;
    public float musicVolume;
    public float FXVolume;

    private void Start()
    {
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        myAudioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        myAudioMixer.SetFloat("FXVolume", Mathf.Log10(FXVolume) * 20);
    }

}
