using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Button;


public class SecondPlayerEnableButton : MonoBehaviour
{
    //This script makes the confirm button interactable and sets the Image to be active
    //when the characters are selected
    public Button SecondPlayer1;
    public Button SecondPlayer2;
    public Button SecondPlayer3;
    public Button SecondPlayer4;
    public Button secondplayerImage;
    public Button confirmButton;



    public void ButtonEnabler()
    {
        secondplayerImage.gameObject.SetActive(true);
        confirmButton.interactable = true;
    }


}