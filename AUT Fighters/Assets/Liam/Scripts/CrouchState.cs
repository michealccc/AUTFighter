using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        character.rb.velocity = new Vector2(0, 0);
        Debug.Log("Entered Crouch State");
    }

    public void Execute()
    {
        if(!character.isCrouching)
        {
            character.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        character.anim.SetBool("IsCrouching", false);
        Debug.Log("Exiting Crouch State");
    }
}
