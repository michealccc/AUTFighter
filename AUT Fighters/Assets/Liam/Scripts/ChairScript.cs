using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairScript : Special
{
    public float moveSpeed;
    public float timeToLive;
    public Rigidbody2D rb;

    void Update()
    {
        DestroySelf();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        };
    }

    private void DestroySelf()
    {
        if(timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        timeToLive -= Time.deltaTime;
    }
}
