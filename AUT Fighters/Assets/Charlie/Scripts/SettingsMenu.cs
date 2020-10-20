using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class SettingsMenu : MonoBehaviour
{
    // Player 1 buttons
    public Button p1Jump;
    public Button p1Crouch;
    public Button p1Left;
    public Button p1Right;
    public Button p1Light;
    public Button p1Med;
    public Button p1Heavy;
    public Button p1Spec;

    // Plauer 2 buttons
    public Button p2Jump;
    public Button p2Crouch;
    public Button p2Left;
    public Button p2Right;
    public Button p2Light;
    public Button p2Med;
    public Button p2Heavy;
    public Button p2Spec;

    private string[] buttons = 
    { 
        "P1Jump", "P1Crouch", "P1Left", "P1Right", "P1Light", "P1Med", "P1Heavy",
        "P2Jump", "P2Crouch", "P2Left", "P2Right", "P2Light", "P2Med", "P2Heavy"
    };

    public AudioMixer mainMixer;

    public InputActionAsset IAA;
    public InputActionRebindingExtensions.RebindingOperation rebindOperation;

    public void setVolume(float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void resetButtonAction()
    {
        // Display original keybinds on buttons
        p1Jump.transform.GetChild(0).GetComponent<TMP_Text>().text = "Space";
        p1Crouch.transform.GetChild(0).GetComponent<TMP_Text>().text = "S";
        p1Left.transform.GetChild(0).GetComponent<TMP_Text>().text = "A";
        p1Right.transform.GetChild(0).GetComponent<TMP_Text>().text = "D";
        p1Light.transform.GetChild(0).GetComponent<TMP_Text>().text = "U";
        p1Med.transform.GetChild(0).GetComponent<TMP_Text>().text = "I";
        p1Heavy.transform.GetChild(0).GetComponent<TMP_Text>().text = "O";

        p2Jump.transform.GetChild(0).GetComponent<TMP_Text>().text = "Up Arrow";
        p2Crouch.transform.GetChild(0).GetComponent<TMP_Text>().text = "Down Arrow";
        p2Left.transform.GetChild(0).GetComponent<TMP_Text>().text = "Left Arrow";
        p2Right.transform.GetChild(0).GetComponent<TMP_Text>().text = "Right Arrow";
        p2Light.transform.GetChild(0).GetComponent<TMP_Text>().text = "Numpad 4";
        p2Med.transform.GetChild(0).GetComponent<TMP_Text>().text = "Numpad 5";
        p2Heavy.transform.GetChild(0).GetComponent<TMP_Text>().text = "Numpad 6";

        // Reset keybinds to original
        //for (int i = 0; i < buttons.Length; i++)
        //{
        //    IAA.FindActionMap(findPlayer(buttons[i])).FindAction(getAction(buttons[i])).Disable();
        //    rebindOperation = IAA.FindActionMap(findPlayer(buttons[i])).FindAction(getAction(buttons[i])).PerformInteractiveRebinding(getActionIndex(buttons[i])).
        //}   
    }

    // Check if button is for player 1 or player 2
    private string findPlayer(string str)
    {
        if (str.Contains("P1"))
        {
            return "Player1";
        }
        else
        {
            return "Player2";
        }
    }

    // Get current move to have rebinded
    private string getAction(string str)
    {
        if (str.Contains("Jump"))
        {
            return "Jump";
        }
        else if (str.Contains("Crouch"))
        {
            return "Crouch";
        }
        else if (str.Contains("Left"))
        {
            return "Walk";
        }
        else if (str.Contains("Right"))
        {
            return "Walk";
        }
        else if (str.Contains("Light"))
        {
            return "LightAttack";
        }
        else if (str.Contains("Med"))
        {
            return "MedAttack";
        }
        else if (str.Contains("Heavy"))
        {
            return "HeavyAttack";
        }
        else
        {
            return null;
        }
    }

    // Get index of move if action is composite
    private int getActionIndex(string str)
    {
        if (str.Contains("Left"))
        {
            return 1;
        }
        else if (str.Contains("Right"))
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    public void backButtonAction()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
