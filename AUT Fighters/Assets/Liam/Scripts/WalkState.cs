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
        character.moveDir = character.inputs.walk.ReadValue<float>();
        character.anim.SetFloat("MoveX", character.moveDir * character.direction);

        character.DirectionToBeFacing();
        character.Walk();

        if (character.inputs.walk.ReadValue<float>() == 0)
        {
            character.ChangeState(new IdleState());
        }

        if (character.inputs.jump.ReadValue<float>() != 0)
        {
            //character.anim.SetBool("IsJumping", true);
            character.ChangeState(new JumpState());
            character.Jump();
        }

        if (character.inputs.crouch.ReadValue<float>() != 0)
        {
            character.ChangeState(new CrouchState());
        }

        character.HandleAttackPress();
    }

    public void Exit()
    {
        character.anim.SetBool("IsWalking", false);
        Debug.Log("Exiting Walking State");
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
            Debug.Log(character.GetHashCode() + "Contact made in walk");
        }
    }
}
