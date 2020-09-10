﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : ICharacterState
{
    private CharacterController character;

    public void Enter(CharacterController controller)
    {
        character = controller;
        character.anim.SetBool("IsJumping", true);
        character.ToggleGroundCollider();
        //character.Jump();
        Debug.Log("Entering Jump State");
    }

    public void Execute()
    {
        if(character.IsLanding() && character.rb.velocity.y < 0)
        {
            character.ToggleGroundCollider();
            character.ChangeState(new IdleState());
        }

        if(!character.airAttackPerformed)
        {
            character.HandleAttackPress();
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Jump State");
        character.anim.SetBool("IsJumping", false);
       // character.anim.SetFloat("MoveX", 0);
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            Debug.Log("Contact made in jump");
        }
    }
}
