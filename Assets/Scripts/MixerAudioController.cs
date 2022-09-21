using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerAudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private MixerManager myMixer;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider FXVolumeSlider;


    private void Start()
    {

        myMixer = GameObject.Find("AudioManager").GetComponent<MixerManager>();

        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(myMixer.masterVolume) * 20);
        myAudioMixer.SetFloat("MusicVolume", Mathf.Log10(myMixer.musicVolume) * 20);
        myAudioMixer.SetFloat("FXVolume", Mathf.Log10(myMixer.FXVolume) * 20);

        MasterVolumeSlider.value = myMixer.masterVolume;
        MusicVolumeSlider.value = myMixer.musicVolume;
        FXVolumeSlider.value = myMixer.FXVolume;
    }

    public void SetMasterVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        myMixer.masterVolume = sliderValue;
    }

    public void SetMusicVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        myMixer.musicVolume = sliderValue;
    }

    public void SetFxVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("FXVolume", Mathf.Log10(sliderValue) * 20);
        myMixer.FXVolume = sliderValue;
    }

}
