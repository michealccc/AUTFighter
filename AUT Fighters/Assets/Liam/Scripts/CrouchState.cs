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
        if (other.CompareTag("Hitbox"))
        {
            if (character.IsBlocking(other.GetComponentInParent<CharacterController>().currentAttackData))
            {
                character.OnBlock(other.GetComponentInParent<CharacterController>());
            }
            else
            {
                character.OnHit(other.GetComponentInParent<CharacterController>());
            }
            Debug.Log(character.GetHashCode() + "Contact made in crouch");
        }
        else if (other.CompareTag("Special"))
        {
            Debug.Log("Hit by special");
            if (character.IsBlocking(other.GetComponent<Special>().atkData))
            {
                character.OnBlock(other.GetComponent<Special>().atkData);
            }
            else
            {
                character.OnHit(other.GetComponent<Special>().atkData);
            }
        }
        else if(other.CompareTag("Throwbox"))
        {
            character.OnThrown(other.GetComponentInParent<CharacterController>());
        }
        else if(other.CompareTag("Landing"))
        {
            character.JumpLandCheck(other.GetComponentInParent<CharacterController>().gameObject);
        }
    }
}
