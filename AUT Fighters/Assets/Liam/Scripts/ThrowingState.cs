using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Throwing State");
        character = controller;
    }

    public void Execute()
    {
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Throwing State");
        character.anim.SetBool("IsThrowing", false);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
