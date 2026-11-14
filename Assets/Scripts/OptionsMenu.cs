using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour{

    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume){
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("SoundVolume", volume);
    }

    public void SetFullScreen(bool fullScreen){
        Screen.fullScreen = fullScreen;
    }

}

