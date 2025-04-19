using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public Slider[] VolumeSliders;
    float currentMasterVolume;
    float currentUIVolume;
    float currentSfxVolume;
    float currentMusicVolume;
    void Start()
    {
        
        MasterMixer.GetFloat("MasterVolume", out currentMasterVolume);
        VolumeSliders[0].value = currentMasterVolume;
        VolumeSliders[0].onValueChanged.AddListener(SetMasterVolume);

        
        MasterMixer.GetFloat("MasterVolume", out currentUIVolume);
        VolumeSliders[1].value = currentUIVolume;
        VolumeSliders[1].onValueChanged.AddListener(SetUIVolume);

        
        MasterMixer.GetFloat("MasterVolume", out currentSfxVolume);
        VolumeSliders[2].value = currentSfxVolume;
        VolumeSliders[2].onValueChanged.AddListener(SetSfxVolume);

        
        MasterMixer.GetFloat("MasterVolume", out currentMusicVolume);
        VolumeSliders[3].value = currentMusicVolume;
        VolumeSliders[3].onValueChanged.AddListener(SetMusicVolume);



    }

    public void SetMasterVolume(float sliderValue)
    {
        MasterMixer.SetFloat("MasterVolume", sliderValue);
    }

    public void SetUIVolume(float sliderValue)
    {
        MasterMixer.SetFloat("UIVolume", sliderValue);
    }

    public void SetSfxVolume(float sliderValue)
    {
        MasterMixer.SetFloat("SfxVolume", sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        MasterMixer.SetFloat("MusicVolume", sliderValue);
    }
}
