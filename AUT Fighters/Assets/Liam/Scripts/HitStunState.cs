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
        character.rb.velocity = new Vector2(0, 0);
        //character.anim.GetCurrentAnimatorClipInfo(0).
    }

    public void Execute()
    {
        HitStunned();
    }

    public void Exit()
    {
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
        Debug.Log(hitDuration);
        if (hitDuration > 0)
        {
            hitDuration -= 0.05f;
            //character.anim.Play("NidStandHit");
        }
        else
        {
            character.anim.SetBool("InHitStun", false);
            character.ChangeState(new IdleState());
        }
    }
}
