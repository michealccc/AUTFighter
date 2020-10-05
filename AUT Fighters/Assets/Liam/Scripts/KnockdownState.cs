using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockdownState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Knockdown State");
        character = controller;
        character.throwHurtbox.enabled = false;
        //character.anim.SetBool("IsKnockedDown", true);
        //character.rb.velocity = new Vector2(0, 0);
        //character.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Execute()
    {
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.ChangeState(new WakeupState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Knockdown State");
        //character.anim.SetBool("IsKnockedDown", false);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
