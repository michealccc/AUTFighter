using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeColourSampleScript : MonoBehaviour
{
    Material material;
    private float sanColourChanged = 0f;
    private float charlieColourChanged = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to materal
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        // Change San colour
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            Debug.Log("In if statement");
            if (sanColourChanged == 0f)
            {
                sanColourChanged = 1f;               
            } 
            else
            {
                sanColourChanged = 0f;
            }
            Debug.Log(sanColourChanged);
            material.SetFloat("_sanColourChanged", sanColourChanged);
            Debug.Log(material);
        }

        // Change Charlie colour
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            if (charlieColourChanged == 0f)
            {
                charlieColourChanged = 1f;
            }
            else
            {
                charlieColourChanged = 0f;
            }
            material.SetFloat("_charlieColourChanged", charlieColourChanged);
            Debug.Log(material.ToString());
        }
    }
}
