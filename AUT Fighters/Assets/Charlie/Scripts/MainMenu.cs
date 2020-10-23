using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audio;

    void Awake()
    {
        //audio = FindObjectOfType<AudioManager>();
        //audio.Play("MenuMusic");
    }

    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        audio.Play("MenuMusic");
    }

    public void ArcadeButtonAction()
    {
        SceneManager.LoadScene("ArcadeScene");
        audio.Play("Confirm");
    }

    //This scene is for Character select in Training Mode and Arcade Mode
    public void TrainingButtonAction()
    {
        SceneManager.LoadScene("TrainingModeCharacterSelect");
    }

    public void OnlineButtonAction()
    {
        SceneManager.LoadScene("OnlineScene");
        audio.Play("Confirm");
    }

    public void CharactersButtonAction()
    {
        SceneManager.LoadScene("CharacterSelect");
        audio.Play("Confirm");
    }

    public void OptionsButtonAction()
    {
        SceneManager.LoadScene("OptionsScene");
        audio.Play("Confirm");
    }

    public void TutorialButtonAction()
    {
        SceneManager.LoadScene("MatchScene 1");
    }

    public void QuitButtonAction()
    {
        Application.Quit();
        audio.Play("Confirm");
    }
}
