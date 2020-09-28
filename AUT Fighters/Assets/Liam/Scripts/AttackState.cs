using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ICharacterState
{
    private CharacterController character;
    //private AttackData[] groundAttacks;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entering Attack State");
        character = controller;
        character.anim.SetBool("IsAttacking", true);
        character.rb.velocity = new Vector2(0, 0);
        if(character.currentAttackData != null)
        {
            character.stats.GainMeter(character.currentAttackData.damage * 0.1f);
        }
    }

    public void Execute()
    {
        //Debug.Log("Normalized Time: " + character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if(character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Attack State");
        character.anim.SetBool("IsAttacking", false);
        character.anim.SetInteger("AttackStrength", 0);
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
