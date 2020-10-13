using System;
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

    private float previousHp;
    private float currentHp;

    public GameObject match;
    public MatchManager mm;

    private KeyCode lastHitKey1;
    private KeyCode lastHitKey2;

    private void Start()
    {
        popUpIndex = 0;

        leftKeyPressed = false;
        rightKeyPressed = false;
        jumpKeyPressed = false;
        crouchKeyPressed = false;

        previousHp = mm.p2.stats.maxHp;
        currentHp = mm.p2.stats.currentHp;
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

    private void OnGUI()
    {
        // Get current hp of player 2
        currentHp = mm.p2.stats.currentHp;

        // Get last key hit
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (Keyboard.current.uKey.wasPressedThisFrame)
            {
                lastHitKey1 = KeyCode.U;    // Light atk
            } 
            else if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                lastHitKey1 = KeyCode.I;    // Medium atk
            }
            else if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                lastHitKey1 = KeyCode.O;    // Heavy atk
            }
            else if (Keyboard.current.jKey.wasPressedThisFrame)
            {
                lastHitKey1 = KeyCode.J;    // Special atk
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
            superAtkPhase();    // Super attack           
        }
        else if (phase == 7)
        {
            grabPhase();        // Grab           
        }
        else
        {
            complete();         // Tutorial complete
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
        if (currentHp != previousHp)
        {
            if (lastHitKey1 == KeyCode.U)
            {
                previousHp = currentHp;
                popUpIndex++;
            }           
        }
    }

    public void medAtkPhase()
    {
        if (currentHp != previousHp)
        {
            if (lastHitKey1 == KeyCode.I)
            {
                previousHp = currentHp;
                popUpIndex++;
            }
        }
    }

    public void heavyAtkPhase()
    {
        if (currentHp != previousHp)
        {
            if (lastHitKey1 == KeyCode.O)
            {
                previousHp = currentHp;
                popUpIndex++;
            }
        }
    }

    public void specialAtkPhase()
    {
        if (currentHp != previousHp)
        {
            if (lastHitKey1 == KeyCode.J)
            {
                previousHp = currentHp;

                mm.p1.stats.ResetSuperMeter();
                mm.p1.stats.GainMeter(100f);

                popUpIndex++;
            }
        }
    }

    public void superAtkPhase()
    {
        if (currentHp != previousHp)
        {
            if (Math.Truncate(mm.p1.stats.currentSuperMeter) == 5)
            {
                previousHp = currentHp;
                popUpIndex++;
            }
        }
    }

    public void grabPhase()
    {
        if (currentHp != previousHp)
        {
            if (lastHitKey1 == KeyCode.U)
            {
                previousHp = currentHp;
                popUpIndex++;
            }
        }
    }

    public void complete()
    {
        if (Keyboard.current[Key.Enter].wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

