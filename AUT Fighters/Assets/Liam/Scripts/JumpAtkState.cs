using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAtkState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entering Jump Atk State");
        character = controller;
        character.anim.SetBool("IsAttacking", true);
        character.anim.SetBool("IsJumping", true);
    }

    public void Execute()
    {
        //if (character.IsLanding() && character.rb.velocity.y < 0)
        //{
        //    character.ChangeState(new IdleState());
        //    Debug.Log("Landing from atk");
        //}
        Debug.Log("Normalized Time for Jump Attack: " + character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if(character.IsLanding() && character.rb.velocity.y <= 0)
        {
            character.ChangeState(new IdleState());
        }
        else if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Debug.Log("Jump attacking is finished");
            character.ChangeState(new JumpState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Jump Atk State");
        character.anim.SetBool("IsJumping", false);
        character.anim.SetBool("IsAttacking", false);
        character.anim.SetInteger("AttackStrength", 0);
        character.airAttackPerformed = true;
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
    }
}
