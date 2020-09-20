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
        blockDuration = atkData.blockStunDuration;
        character.opponent.rb.AddForce(character.transform.right * -character.opponent.direction * character.opponent.currentAttackData.pushback, ForceMode2D.Impulse);
        character.rb.AddForce(character.transform.right * -character.direction * character.opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
        //character.rb.AddForce(character.transform.right * character.direction * opponent.currentAttackData.pushforward, ForceMode2D.Impulse);
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
        if (other.CompareTag("Hitbox"))
        {
            if(character.IsBlocking(other.GetComponent<Special>().atkData))
            {
                character.OnBlock(other.GetComponentInParent<CharacterController>());
            }
            else
            {
                character.OnHit(other.GetComponentInParent<CharacterController>());
            }
            Debug.Log(character.GetHashCode() + "Contact made in blocking");
        }
    }

    private void BlockStunned()
    {
        if(blockDuration > 0)
        {
            Debug.Log("Still Blocking");
            blockDuration -= 0.05f;
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
