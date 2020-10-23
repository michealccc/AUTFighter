using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    public Button confirmbutton;
    // Start is called before the first frame update
    void Start()
    {
        confirmbutton.interactable = false;
    }

    // Update is called once per frame
    public void enableButton()
    {
        confirmbutton.interactable = true;

    }
}

