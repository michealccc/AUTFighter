using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        character.throwHurtbox.enabled = true;
        character.rb.velocity = new Vector2(0, character.rb.velocity.y);
        character.airAttackPerformed = false;
        character.anim.SetInteger("AttackStrength", 0);
        character.moveDir = 0;
        //character.isMoving = false;
        Debug.Log("Entered Idle State");
    }

    public void Execute()
    {
        character.DirectionToBeFacing();
        character.rb.velocity = new Vector2(0, character.rb.velocity.y);

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
        if (other.CompareTag("Attack"))
        {
            Debug.Log("Being attacked!!!");
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
        //else if(other.CompareTag("Special"))
        //{
        //    Debug.Log("Hit by special");
        //    if (character.IsBlocking(other.GetComponent<Special>().atkData))
        //    {
        //        character.OnBlock(other.GetComponent<Special>().atkData);
        //    }
        //    else
        //    {
        //        character.OnHit(other.GetComponent<Special>().atkData);
        //    }
        //}
        else if(other.CompareTag("Throwbox"))
        {
            Debug.Log("Being thrown!!!");
            character.OnThrown(other.GetComponent<AttackData>());
        }
        else if(other.CompareTag("Landing"))
        {
            character.JumpLandCheck(other.GetComponentInParent<CharacterController>().gameObject);
        }
    }
}
