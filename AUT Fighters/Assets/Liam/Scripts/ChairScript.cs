using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairScript : Special
{
    public float moveSpeed;
    public float timeToLive;
    public Rigidbody2D rb;

    void Start()
    {
        StartCoroutine(Decay(timeToLive));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        };
    }
}
