using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownState : ICharacterState
{
    private CharacterController character;
    private CharacterController opponent;

    public ThrownState(CharacterController opponent)
    {
        this.opponent = opponent;
    }

    public void Enter(CharacterController controller)
    {
        character = controller;
        Debug.Log("Entered Thrown State");
        //character.collider.enabled = false;
    }

    public void Execute()
    {
        FollowThrowPos();
        if(character.anim.GetBool("IsThrown") == false)
        {
            character.stats.TakeDamage(opponent.currentAttackData.damage);
            character.ChangeState(new LaunchState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Thrown State");
        Debug.Log("Throw force: " + new Vector2(character.direction * opponent.currentAttackData.launchForce.x, 1 * opponent.currentAttackData.launchForce.y));
        character.rb.AddForce(new Vector2(character.direction * opponent.currentAttackData.launchForce.x, 1 * opponent.currentAttackData.launchForce.y), ForceMode2D.Impulse);
        //character.collider.enabled = true;
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void FollowThrowPos()
    {
        character.transform.position = character.opponent.transform.Find("ThrowPos").transform.position;
    }
}
