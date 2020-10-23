using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    public Slider musicVolume;
    public Slider fxVolume;

    // Update is called once per frame
    void Update()
    {
        AudioManager.BGMVolume = musicVolume.value;
        AudioManager.FXVolume = fxVolume.value;
    }
}
