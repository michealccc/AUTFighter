using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Wakeup State");
        character = controller;
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
        Debug.Log("Exiting wakeup state");
        //character.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
