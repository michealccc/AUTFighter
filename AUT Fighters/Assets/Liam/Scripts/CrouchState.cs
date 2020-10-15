using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        if(!character.anim.GetBool("IsCrouching"))
        {
            character.anim.SetBool("IsCrouching", true);
        }
        character.throwHurtbox.enabled = true;
        character.rb.velocity = new Vector2(0, 0);
        Debug.Log("Entered Crouch State");
    }

    public void Execute()
    {
        character.DirectionToBeFacing();
        character.rb.velocity = new Vector2(0, 0);
        if(character.inputs.crouch.ReadValue<float>() == 0)
        {
            character.ChangeState(new IdleState());
        }

        if(character.inputs.jump.ReadValue<float>() != 0)
        {
            character.Jump();
            character.ChangeState(new JumpState());
        }

        character.HandleAttackPress();
    }

    public void Exit()
    {
        Debug.Log("Exiting Crouch State");
        //Check to see if the character is attacking while crouched, if they are, do not set crouching to false because there are crouching attacks
        if(character.anim.GetInteger("AttackStrength") == 0)
        {
            character.anim.SetBool("IsCrouching", false);
        }
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            AttackData atk = other.GetComponent<AttackData>();
            if (character.IsBlocking(atk))
            {
                character.OnBlock(atk);
            }
            else
            {
                character.OnHit(atk);
            }
        }
        else if(other.CompareTag("Throwbox"))
        {
            character.OnThrown(other.GetComponent<AttackData>());
        }
        else if(other.CompareTag("Landing"))
        {
            character.JumpLandCheck(other.GetComponentInParent<CharacterController>().gameObject);
        }
    }
}
