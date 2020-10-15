using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer mainMixer;

    public void setVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void backButtonAction()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
