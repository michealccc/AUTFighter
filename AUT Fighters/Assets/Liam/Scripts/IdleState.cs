using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        character.rb.velocity = new Vector2(0, character.rb.velocity.y);
        //character.isMoving = false;
        Debug.Log("Entered Idle State");
    }

    public void Execute()
    {
        if(character.isMoving)
        {
            character.anim.SetBool("IsWalking", true);
            character.ChangeState(new WalkState());
        }

        if(character.isJumping)
        {
            character.anim.SetBool("IsJumping", true);
            character.ChangeState(new JumpState());
        }

        if(character.isCrouching)
        {
            character.anim.SetBool("IsCrouching", true);
            character.ChangeState(new CrouchState());
        }

    }

    public void Exit()
    {
        
    }

}
