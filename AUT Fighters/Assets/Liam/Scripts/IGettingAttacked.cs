using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGettingAttacked
{
    void OnHit(CharacterController opponent);
    void OnBlock(CharacterController opponent);
}
