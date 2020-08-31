using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        character = controller;
        character.Jump();
        Debug.Log("Entering Jump State");
    }

    public void Execute()
    {
        if(character.IsLanding() && character.rb.velocity.y < 0)
        {
            character.ChangeState(new IdleState());
            Debug.Log("Landing...");
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Jump State");
        character.anim.SetBool("IsJumping", false);
    }

    //public bool IsLanding()
    //{
    //    float extraHeightCheck = 0.10f;
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(character.collider.bounds.center, character.collider.bounds.size, 0f, Vector2.down, extraHeightCheck, character.platformLayer);
    //    Debug.Log("Raycast Grounded: " + raycastHit.collider);
    //    //Return the raycast collider if it isn't null
    //    return raycastHit.collider != null;
    //}
}
