using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAttackState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entering Crouch Attack State");
        character = controller;
        character.anim.SetBool("IsAttacking", true);
        character.anim.SetBool("IsCrouching", true);
        character.rb.velocity = new Vector2(0, 0);
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
        Debug.Log("Exiting Crouch Attack State");
        character.anim.SetBool("IsAttacking", false);
        //If the character is still crouching after doing a crouching attack, remain crouched
        if(character.inputs.crouch.ReadValue<float>() == 0)
        {
            character.anim.SetBool("IsCrouching", false);
        }
        character.anim.SetInteger("AttackStrength", 0);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            Debug.Log("Contact made in attack");
        }
    }
}
