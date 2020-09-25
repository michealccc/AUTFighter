using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMenu : MonoBehaviour
{
    // Begins the fight match
    public void FightButtonAction()
    {
        SceneManager.LoadScene("Micheal/Scenes/FightScene");
    }
}