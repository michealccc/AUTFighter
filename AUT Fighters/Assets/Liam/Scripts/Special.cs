using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public AttackData atkData;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
