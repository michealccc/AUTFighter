using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public float maxHp;
    public float currentHp;
    public float maxSuperMeter;
    public float currentSuperMeter;

    public void ResetHp()
    {
        currentHp = maxHp;
    }

    public void ResetSuperMeter()
    {
        currentSuperMeter = 0f;
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        if(currentHp < 0)
        {
            currentHp = 0f;
        }
    }

    public void GainMeter(float gain)
    {
        currentSuperMeter += gain;
        if(currentSuperMeter > maxSuperMeter)
        {
            currentSuperMeter = maxSuperMeter;
        }
    }
}
