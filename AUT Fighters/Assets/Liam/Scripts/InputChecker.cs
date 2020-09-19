using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputChecker : MonoBehaviour
{
    public InputAction walk;
    public InputAction jump;
    public InputAction crouch;
    public InputAction light;
    public InputAction med;
    public InputAction heavy;
    public InputAction special;
    // Start is called before the first frame update

    void Start()
    {
        walk = GetComponent<PlayerInput>().currentActionMap.FindAction("Walk");
        jump = GetComponent<PlayerInput>().currentActionMap.FindAction("Jump");
        crouch = GetComponent<PlayerInput>().currentActionMap.FindAction("Crouch");
        light = GetComponent<PlayerInput>().currentActionMap.FindAction("LightAttack");
        med = GetComponent<PlayerInput>().currentActionMap.FindAction("MedAttack");
        heavy = GetComponent<PlayerInput>().currentActionMap.FindAction("HeavyAttack");

    }

}
