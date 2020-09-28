using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGettingAttacked
{
    void OnHit(AttackData theAtk);
    void OnBlock(AttackData theAtk);
    void OnThrown(CharacterController opponent);
}
