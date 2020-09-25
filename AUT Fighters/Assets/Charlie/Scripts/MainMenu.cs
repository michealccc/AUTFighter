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

    public void QuitButtonAction()
    {
        Application.Quit();
    }
}
