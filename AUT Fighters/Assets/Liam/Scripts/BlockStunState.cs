using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStunState : ICharacterState
{
    private CharacterController character;
    private AttackData atkData;
    private float blockDuration;
    //private CharacterController opponent;

    public BlockStunState(AttackData atk)
    {
        atkData = atk;
        //blockDuration = attack.blockStunDuration;
    }

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Block Stun State" + atkData);
        character = controller;
        character.throwHurtbox.enabled = false;
        blockDuration = atkData.blockStunDuration;

        character.opponent.rb.AddForce(character.transform.right * -character.opponent.direction * atkData.pushback, ForceMode2D.Impulse);
        character.rb.AddForce(character.transform.right * -character.direction *  atkData.pushforward, ForceMode2D.Impulse);
    }

    public void Execute()
    {
        BlockStunned();
    }

    public void Exit()
    {
        character.anim.SetBool("InBlockStun", false);
        Debug.Log("Exiting Block Stun State");
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

    private void BlockStunned()
    {
        if(blockDuration > 0)
        {
            Debug.Log("Still Blocking");
            blockDuration -= 1.5f * Time.fixedDeltaTime;
        }
        else
        {
            //character.ChangeState(new IdleState());
            if (character.inputs.crouch.ReadValue<float>() != 0)
            {
                Debug.Log("Maintain crouch from block");
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
