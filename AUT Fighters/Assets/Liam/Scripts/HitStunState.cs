using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStunState : ICharacterState
{
    private CharacterController character;
    private AttackData atkData;
    private float hitDuration;
    //private CharacterController opponent;

    public HitStunState(AttackData atk)
    {
        atkData = atk;
    }

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Hit Stun State" + atkData);
        character = controller;
        character.throwHurtbox.enabled = false;
        hitDuration = atkData.hitStunDuration;

        character.opponent.rb.AddForce(character.transform.right * -character.opponent.direction * atkData.pushback, ForceMode2D.Impulse);
        character.rb.AddForce(character.transform.right * -character.direction * atkData.pushforward, ForceMode2D.Impulse);

        Debug.Log("Character velocity from hitstun: " + character.rb.velocity);
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

    private void HitStunned()
    {
        if (hitDuration > 0)
        {
            hitDuration -= 1.5f * Time.fixedDeltaTime;
            //character.anim.Play("NidStandHit");
        }
        else
        {
            character.anim.SetBool("InHitStun", false);
            if(character.inputs.crouch.ReadValue<float>() != 0)
            {
                Debug.Log("Maintain crouch from hit");
                character.anim.SetBool("IsCrouching", true);
                character.ChangeState(new CrouchState());
            }
            else
            {
                character.ChangeState(new IdleState());
            }
        }
    }
}
