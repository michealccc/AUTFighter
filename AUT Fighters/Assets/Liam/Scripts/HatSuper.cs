using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSuper : Special
{
    public float returnTimer;
    public float moveSpeed;
    public Rigidbody2D rb;
    private bool isReturning = false;

    void Start()
    {
        StartCoroutine(HatReturn(returnTimer));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isReturning)
        {
            Vector2 dir = (GetComponent<AttackData>().origin.transform.position - transform.position).normalized;
            rb.velocity = dir * moveSpeed * Time.deltaTime;
        }
    }

    private IEnumerator HatReturn(float timer)
    {
        yield return new WaitForSeconds(returnTimer);
        isReturning = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isReturning)
        {
            if(other.gameObject == GetComponent<AttackData>().origin.gameObject)
            {
                DestroySelf();
            }
        }
    }
}
