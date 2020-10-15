using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NidSuper : MonoBehaviour
{
    public Transform tfm;
    public SuperChairScript[] superChairs;
    public float timeToLive;
    public CharacterController origin;
    private float maxTimeToLive;
    private Transform opponentPos;
    private bool rotate;
    // Start is called before the first frame update
    void Start()
    {
        SetSelfReferenceForChairs();
        opponentPos = this.GetComponentInParent<CharacterController>().opponent.GetComponent<Transform>();
        maxTimeToLive = timeToLive;
        rotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        DestroySelf();
        RotateChairs();
        ShootChairs();
    }

    public void RotateChairs()
    {
        if(rotate)
        {
            tfm.Rotate(0, 0, 1f);
        }
    }

    private void SetSelfReferenceForChairs()
    {
        foreach(SuperChairScript chair in superChairs)
        {
            if(chair != null)
            {
                //Debug.Log("The chair's parent " + this.GetComponentInParent<CharacterController>().gameObject);
                //chair.self = this.GetComponentInParent<CharacterController>().gameObject;
                chair.GetComponent<AttackData>().origin = origin;
                chair.origin = origin;
            }
        }
    }

    private void ShootChairs()
    {
        if (timeToLive <= maxTimeToLive / 2)
        {
            rotate = false;
            foreach (SuperChairScript chair in superChairs)
            {
                if (chair != null)
                {
                    Vector2 direction = (opponentPos.position - chair.transform.position).normalized;
                    chair.rb.velocity = direction * chair.moveSpeed;
                }
            }
        }

        //foreach (SuperChairScript chair in superChairs)
        //{
        //    if (chair != null)
        //    {
        //        Vector2 direction = (opponentPos.position - chair.transform.position).normalized;
        //        chair.rb.velocity = direction * chair.moveSpeed;
        //    }
        //}
    }

    private void DestroySelf()
    {
        if(timeToLive <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeToLive -= Time.deltaTime;
        }
    }
}
