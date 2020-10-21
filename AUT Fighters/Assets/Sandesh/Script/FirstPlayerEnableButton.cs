using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Button;


public class FirstPlayerEnableButton : MonoBehaviour
{
    //This script makes the confirm button interactable and sets the Image to be active
    //when the characters are selected
    public Button Player1;
    public Button Player2;
    public Button Player3;
    public Button Player4;
    public Button firstplayerImage;
    public Button confirmButton;


    public void ButtonEnabler()
    {
        firstplayerImage.gameObject.SetActive(true);
        confirmButton.interactable = true;

    }


}