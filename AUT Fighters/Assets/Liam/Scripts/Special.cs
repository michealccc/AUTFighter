using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public AttackData atkData;

    public IEnumerator Decay(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
