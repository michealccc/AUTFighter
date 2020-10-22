using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ArcadeButtonAction()
    {
        SceneManager.LoadScene("ArcadeScene");
    }

    //This scene is for Character select in Training Mode and Arcade Mode
    public void TrainingButtonAction()
    {
        SceneManager.LoadScene("TrainingModeCharacterSelect");
    }

    public void OnlineButtonAction()
    {
        SceneManager.LoadScene("OnlineScene");
    }

    public void CharactersButtonAction()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void OptionsButtonAction()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void TutorialButtonAction()
    {
        SceneManager.LoadScene("MatchScene 1");
    }

    public void QuitButtonAction()
    {
        Application.Quit();
    }
}
