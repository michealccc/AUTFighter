using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        character = controller;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }
}
