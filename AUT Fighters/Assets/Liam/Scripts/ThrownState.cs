using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownState : ICharacterState
{
    private CharacterController character;
    //private CharacterController opponent;
    private AttackData atkData;

    public ThrownState(AttackData atk)
    {
        atkData = atk;
    }

    public void Enter(CharacterController controller)
    {
        character = controller;
        Debug.Log("Entered Thrown State");
    }

    public void Execute()
    {
        FollowThrowPos();
        if(character.anim.GetBool("IsThrown") == false)
        {
            character.stats.TakeDamage(atkData.damage);
            character.ChangeState(new LaunchState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Thrown State");
        Debug.Log("Throw force: " + new Vector2(character.direction * atkData.launchForce.x, 1 * atkData.launchForce.y));
        character.rb.AddForce(new Vector2(character.direction * atkData.launchForce.x, 1 * atkData.launchForce.y), ForceMode2D.Impulse);
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
