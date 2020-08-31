using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface for character states
public interface ICharacterState
{
    void Enter(CharacterController controller);
    void Execute();
    void Exit();
}
