using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        character.anim.SetBool("IsWalking", true);
        Debug.Log("Entered Walking State");
    }

    public void Execute()
    {
        character.DirectionToBeFacing();
        character.Walk();
        if(!character.isMoving)
        {
            character.ChangeState(new IdleState());
        }

        if (character.isJumping)
        {
            character.anim.SetBool("IsJumping", true);
            character.ChangeState(new JumpState());
            character.Jump();
        }

        if (character.isCrouching)
        {
            //character.anim.SetBool("IsCrouching", true);
            character.ChangeState(new CrouchState());
        }

        character.HandleAttackPress();
    }

    public void Exit()
    {
        //character.rb.velocity = new Vector2(0, 0);
        character.anim.SetBool("IsWalking", false);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            if (character.IsBlocking())
            {
                character.OnBlock(other.GetComponentInParent<CharacterController>());
            }
            Debug.Log(character.GetHashCode() + "Contact made in walk");
        }
    }
}
