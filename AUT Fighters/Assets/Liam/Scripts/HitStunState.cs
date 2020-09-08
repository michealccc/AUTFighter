using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStunState : ICharacterState
{
    private CharacterController character;
    private AttackData atkData;
    private float hitDuration;
    private CharacterController opponent;

    public HitStunState(CharacterController opponent)
    {
        atkData = opponent.currentAttackData;
        this.opponent = opponent;
    }

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Hit Stun State" + atkData);
        character = controller;
        hitDuration = atkData.hitStunDuration;
    }

    public void Execute()
    {
        HitStunned();
    }

    public void Exit()
    {
        character.anim.SetBool("InHitStun", false);
        Debug.Log("Exiting Hit Stun State");
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
           character.OnHit(other.GetComponentInParent<CharacterController>());
           Debug.Log(character.GetHashCode() + "Contact made in hit stun");
        }
    }

    private void HitStunned()
    {
        if (hitDuration > 0)
        {
            hitDuration -= 0.05f;
        }
        else
        {
            character.ChangeState(new IdleState());
        }
    }
}
