using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : Special
{
    public float moveSpeed;
    public float timeToLive;
    public Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        };
    }

    public void SetDirection(float dir)
    {
        transform.localScale = new Vector2(dir * transform.localScale.x, transform.localScale.y);
    }

    private void Decay()
    {
        if(timeToLive <= 0)
        {
            DestroySelf();
        }
        else
        {
            timeToLive -= Time.deltaTime;
        }
    }
}
