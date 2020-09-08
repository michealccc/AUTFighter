using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        character.anim.SetBool("IsCrouching", true);
        character.rb.velocity = new Vector2(0, 0);
        Debug.Log("Entered Crouch State");
    }

    public void Execute()
    {
        if(!character.isCrouching)
        {
            //character.anim.SetBool("IsCrouching", false);
            character.ChangeState(new IdleState());
        }

        if(character.isJumping)
        {
            character.Jump();
            character.ChangeState(new JumpState());
        }

        character.HandleAttackPress();
    }

    public void Exit()
    {
        Debug.Log("Exiting Crouch State");
        //character.anim.SetBool("IsCrouching", false);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            if (character.IsBlocking())
            {
                character.OnBlock(other.GetComponentInParent<CharacterController>());
            }
            else
            {
                character.OnHit(other.GetComponentInParent<CharacterController>());
            }
            //Debug.Log(character.GetHashCode() + "Contact made in crouch");
        }
    }
}
