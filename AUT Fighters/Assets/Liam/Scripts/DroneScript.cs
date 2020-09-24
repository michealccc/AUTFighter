using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public GameObject droneBeamPrefab;
    public float direction;
    public float timeToLive;
    private float halfLife;
    private Vector2 destination;
    private bool destReached;
    // Start is called before the first frame update
    void Start()
    {
        halfLife = timeToLive / 2;
        destReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        FireBeam();
        Decay();
    }

    public void Decay()
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

    public void SetDirection(float dir)
    {
        direction = dir;
        transform.localScale = new Vector2(direction * transform.localScale.x, transform.localScale.y);
    }

    private void FireBeam()
    {
        if(timeToLive <= halfLife)
        {
            GameObject droneBeamInstance = Instantiate(droneBeamPrefab, transform.position + new Vector3(direction * 2f, 0, 0), transform.rotation);
        }
    }
}
