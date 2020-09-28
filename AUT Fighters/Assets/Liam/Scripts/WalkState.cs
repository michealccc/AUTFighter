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
        else if (other.CompareTag("Throwbox"))
        {
            character.OnThrown(other.GetComponentInParent<CharacterController>());
        }
        else if (other.CompareTag("Landing"))
        {
            character.JumpLandCheck(other.GetComponentInParent<CharacterController>().gameObject);
        }
    }
}
