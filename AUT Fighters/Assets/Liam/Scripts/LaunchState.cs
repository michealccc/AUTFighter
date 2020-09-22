using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Launch State");
        character = controller;
        character.anim.SetBool("IsLaunched", true);
        //character.rb.AddForce(new Vector2(-character.direction * character.opponent.currentAttackData.launchForce.x, 1 * character.opponent.currentAttackData.launchForce.y), ForceMode2D.Impulse); //Maybe shift this into kncokdown state, change the argument for the constructor
    }

    public void Execute()
    {
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && character.IsGrounded() && character.rb.velocity.y <= 0)
        {
            character.ChangeState(new WakeupState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Launch State");
        character.anim.SetBool("IsLaunched", false);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
