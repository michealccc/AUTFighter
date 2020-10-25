using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFire : Special
{
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    public float moveSpeed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            collider.enabled = false;
        }
    }
}
