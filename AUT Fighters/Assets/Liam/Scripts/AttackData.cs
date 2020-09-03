using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    HIGH,
    LOW,
    OVERHEAD
}

public class AttackData
{
    string attackName;
    float damage;
    float pushback;
    float pushforward;
    float blockStunDuration;
    float hitStunDuration;
    bool causeKnockdown;
    AttackType attackType;
}
