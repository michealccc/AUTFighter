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
    public string attackName;
    public float damage;
    public float pushback;
    public float pushforward;
    public Vector2 launchForce;
    public float blockStunDuration;
    public float hitStunDuration;
    public bool causeKnockdown;
    public AttackType attackType;
}
