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
        character.airAttackPerformed = false;
        //character.isMoving = false;
        Debug.Log("Entered Idle State");
    }

    public void Execute()
    {
        character.DirectionToBeFacing();

        if(character.isMoving)
        {
            character.ChangeState(new WalkState());
        }

        if(character.isJumping)
        {
            character.ChangeState(new JumpState());
            character.Jump();
        }

        if(character.isCrouching)
        {
            character.ChangeState(new CrouchState());
        }

        character.HandleAttackPress();
    }

    public void Exit()
    {
        
    }

}
