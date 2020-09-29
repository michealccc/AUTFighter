using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    HIGH,
    LOW,
    OVERHEAD
}

[System.Serializable]
public class AttackData : MonoBehaviour
{
    public CharacterController origin;
    public string attackName;
    public float damage;
    public float pushback;
    public float pushforward;
    public Vector2 launchForce;
    public float blockStunDuration;
    public float hitStunDuration;
    public bool causeKnockdown;
    public AttackType attackType;

    public void SetAttackData(AttackData data)
    {
        origin = data.origin;
        attackName = data.attackName;
        damage = data.damage;
        pushback = data.pushback;
        launchForce = data.launchForce;
        blockStunDuration = data.blockStunDuration;
        hitStunDuration = data.hitStunDuration;
        causeKnockdown = data.causeKnockdown;
        attackType = data.attackType;
    }
}
