using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairScript : Special
{
    public float moveSpeed;
    public Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
