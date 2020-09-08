using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStunState : ICharacterState
{
    private CharacterController character;
    private AttackData atkData;
    private float blockDuration;
    private CharacterController opponent;

    public BlockStunState(CharacterController opponent)
    {
        atkData = opponent.currentAttackData;
        this.opponent = opponent;
        //blockDuration = attack.blockStunDuration;
    }

    public void Enter(CharacterController controller)
    {
        Debug.Log("Entered Block Stun State" + atkData);
        character = controller;
        blockDuration = atkData.blockStunDuration;
        Debug.Log("Push forward force: " + character.transform.right * character.direction * opponent.currentAttackData.pushforward);
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
            if(character.IsBlocking())
            {
                character.OnBlock(other.GetComponentInParent<CharacterController>());
            }
            Debug.Log(character.GetHashCode() + "Contact made in blocking");
        }
    }

    private void BlockStunned()
    {
        if(blockDuration > 0)
        {
            blockDuration -= 0.05f;
        }
        else
        {
            character.ChangeState(new IdleState());
        }
    }
}
