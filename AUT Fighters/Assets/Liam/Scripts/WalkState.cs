using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        Debug.Log("Entered Walking State");
    }

    public void Execute()
    {
        character.Walk();
        if(!character.isMoving)
        {
            character.ChangeState(new IdleState());
        }

        if (character.isJumping)
        {
            character.anim.SetBool("IsJumping", true);
            character.ChangeState(new JumpState());
        }

        if (character.isCrouching)
        {
            character.anim.SetBool("IsCrouching", true);
            character.ChangeState(new CrouchState());
        }
    }

    public void Exit()
    {
        character.anim.SetBool("IsWalking", false);
    }
}
