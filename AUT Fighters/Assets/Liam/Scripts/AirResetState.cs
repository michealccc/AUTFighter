using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirResetState : ICharacterState
{
    private CharacterController character;
    public void Enter(CharacterController controller)
    {
        character = controller;
        character.throwHurtbox.enabled = false;
        character.anim.SetBool("IsAirReset", true);
        character.rb.AddForce(new Vector2(25 * -character.direction, 15), ForceMode2D.Impulse);
    }

    public void Execute()
    {
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && character.IsLanding() && character.rb.velocity.y <= 0)
        {
            character.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        character.anim.SetBool("IsAirReset", false);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
