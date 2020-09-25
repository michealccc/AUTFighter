using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        character.anim.SetBool("IsKO", true);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        character.anim.SetBool("IsKO", false);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
