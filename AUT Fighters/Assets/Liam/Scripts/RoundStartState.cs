using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStartState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        character = controller;
        character.rb.velocity = new Vector2(0, 0);
    }

    public void Execute()
    {
        character.DirectionToBeFacing();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }
}
