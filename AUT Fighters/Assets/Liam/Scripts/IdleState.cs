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
        //character.moveDir = 0;
        //character.isMoving = false;
        Debug.Log("Entered Idle State");
    }

    public void Execute()
    {
        character.DirectionToBeFacing();

        if(character.inputs.walk.ReadValue<float>() != 0)
        {
            character.ChangeState(new WalkState());
        }

        if(character.inputs.jump.ReadValue<float>() != 0)
        {
            character.ChangeState(new JumpState());
            character.Jump();
        }

        if(character.inputs.crouch.ReadValue<float>() != 0)
        {
            character.ChangeState(new CrouchState());
        }

        character.HandleAttackPress();
    }

    public void Exit()
    {
        Debug.Log("Exiting Idle State");
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            if(character.IsBlocking())
            {
                character.OnBlock(other.GetComponentInParent<CharacterController>());
            }
            else
            {
                character.OnHit(other.GetComponentInParent<CharacterController>());
            }
            Debug.Log(character.GetHashCode() + "Contact made in idle");
        }
    }
}
