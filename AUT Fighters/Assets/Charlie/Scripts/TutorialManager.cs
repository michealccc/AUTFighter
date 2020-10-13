using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    private bool leftKeyPressed;
    private bool rightKeyPressed;
    private bool jumpKeyPressed;
    private bool crouchKeyPressed;
    private bool lightAtkHit;
    private bool medAtkHit;
    private bool heavyAtkHit;
    private bool specialAtkHit;
    private bool rageAtkHit;
    private bool grabHit;

    private void Start()
    {
        popUpIndex = 0;
        leftKeyPressed = false;
        rightKeyPressed = false;
        jumpKeyPressed = false;
        crouchKeyPressed = false;
        lightAtkHit = false;
        medAtkHit = false;
        heavyAtkHit = false;
        specialAtkHit = false;
        rageAtkHit = false;
        grabHit = false;
    }

    private void Update()
    {
        // Display pop in game view
        for (int i = 0; i < popUps.Length; i++)
        {
            if(i == popUpIndex)
            {
                // Show next instruction
                popUps[i].SetActive(true);
                tutorialPhase(popUpIndex);
            } else
            {
                // Hide all other instructions
                popUps[i].SetActive(false);
            }
        }
    }

    public void tutorialPhase(int phase)
    {
        if (phase == 0)
        {
            movementPhase();    // Left/right movement 
        }
        else if (phase == 1)
        {
            jumpPhase();        // Up/down movement 
        }
        else if (phase == 2)
        {
            lightAtkPhase();    // Light attack    
        }
        else if (phase == 3)
        {
            medAtkPhase();      // Medium attack            
        }
        else if (phase == 4)
        {
            heavyAtkPhase();    // Heavy attack            
        }
        else if (phase == 5)
        {
            specialAtkPhase();  // Special attack            
        } 
        else if (phase == 6)
        {
            rageAtkPhase();     // Rage attack           
        }
        else if (phase == 7)
        {
            grabPhase();        // Grab           
        }
    }

    public void movementPhase()
    {
        if (leftKeyPressed && rightKeyPressed)
        {
            popUpIndex++;
        }
        else if (Keyboard.current[Key.A].wasPressedThisFrame)
        {
            leftKeyPressed = true;
        }
        else if (Keyboard.current[Key.D].wasPressedThisFrame)
        {
            rightKeyPressed = true;
        }
    }

    public void jumpPhase()
    {
        if (jumpKeyPressed && crouchKeyPressed)
        {
            popUpIndex++;
        } 
        else if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            jumpKeyPressed = true;
        } 
        else if (Keyboard.current[Key.S].wasPressedThisFrame)
        {
            crouchKeyPressed = true;
        }
    }

    public void lightAtkPhase()
    {
        if (Keyboard.current[Key.U].wasPressedThisFrame)
        {
            popUpIndex++;
        }
    }

    public void medAtkPhase()
    {
        if (Keyboard.current[Key.I].wasPressedThisFrame)
        {
            popUpIndex++;
        }
    }

    public void heavyAtkPhase()
    {
        if (Keyboard.current[Key.O].wasPressedThisFrame)
        {
            popUpIndex++;
        }
    }

    public void specialAtkPhase()
    {
        if (Keyboard.current[Key.J].wasPressedThisFrame)
        {
            popUpIndex++;
        }
    }

    public void rageAtkPhase()
    {
        if (Keyboard.current[Key.J].wasPressedThisFrame && Keyboard.current[Key.O].wasPressedThisFrame)
        {
            popUpIndex++;
        }
    }

    public void grabPhase()
    {
        if (Keyboard.current[Key.U].wasPressedThisFrame && Keyboard.current[Key.I].wasPressedThisFrame)
        {
            popUpIndex++;
        }
    }
}

