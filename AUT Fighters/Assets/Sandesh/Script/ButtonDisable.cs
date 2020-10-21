using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonDisable : MonoBehaviour
{
    //This Script makes the confirm button uninteractable until the character buttons are selected
    //Also it hides the image before any characters are selected
    public Button button;
    public Button confirmbutton;
    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(false);
        confirmbutton.interactable = false;

    }

}
