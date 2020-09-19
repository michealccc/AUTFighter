using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Throw State");
        character = controller;
        character.anim.SetBool("IsAttacking", true);
        character.rb.velocity = new Vector2(0, 0);
    }

    public void Execute()
    {
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && character.anim.GetBool("IsThrowing") == true)
        {
            character.ChangeState(new ThrowingState());
        }
        else if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && character.anim.GetBool("IsThrowing") == false)
        {
            character.anim.SetBool("IsThrowing", false);
            character.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Throw State");
        character.anim.SetBool("IsAttacking", false);
        character.anim.SetInteger("AttackStrength", 0);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
