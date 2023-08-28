using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] public AudioMixer musicAudioMixer;
    [SerializeField] public AudioMixer SFXAudioMixer;


    public void SetVolumeMusic(float vol) 
    {
        musicAudioMixer.SetFloat("Volume", vol);
    }

    public void SetVolumeSFX(float vol)
    {
        SFXAudioMixer.SetFloat("Volume", vol);
    }

    public void SetFullscreen(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }

}
