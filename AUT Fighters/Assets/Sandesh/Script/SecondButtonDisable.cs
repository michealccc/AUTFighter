using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SecondButtonDisable : MonoBehaviour
{
    //This Script makes the confirm button uninteractable until the second character buttons are selected
    //Also it hides the image before any characters are selected

    public Button secondcharacterbutton;
    public Button confirmbutton;
    // Start is called before the first frame update
    void Start()
    {
        secondcharacterbutton.gameObject.SetActive(false);
        confirmbutton.interactable = false;

    }

}