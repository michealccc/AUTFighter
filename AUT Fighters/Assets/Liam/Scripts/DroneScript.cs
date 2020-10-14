using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public CharacterController origin;
    public DroneBeamScript droneBeamPrefab;
    public float direction;
    public float timeToLive;
    public Vector2 startPoint;
    public Vector2 destination;
    private float halfLife;
    private bool destReached;
    private bool beamFired;
    private AudioManager audio;
    // Start is called before the first frame update
    void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        startPoint = transform.position;
        //halfLife = timeToLive / 2;
        destReached = false;
        StartCoroutine(MoveToDest());
    }

    // Update is called once per frame
    void Update()
    {
        if(destReached)
        {
            if(!beamFired)
            {
                StartCoroutine(FireBeam());
            }
        }
    }

    private IEnumerator Decay(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        transform.localScale = new Vector2(direction * transform.localScale.x, transform.localScale.y);
    }

    private IEnumerator FireBeam()
    {
        beamFired = true;
        yield return new WaitForSeconds(1f);
        DroneBeamScript droneBeamInstance = Instantiate(droneBeamPrefab, transform.position + new Vector3(direction * 2f, 0, 0), transform.rotation);
        droneBeamInstance.transform.parent = transform;
        droneBeamInstance.GetComponent<AttackData>().origin = origin;
        audio.Play("LaserSound");

        StartCoroutine(Decay(timeToLive));
    }

    private IEnumerator MoveToDest()
    {
        float time = 0;

         while(time < 1)
         {                                              // until one second passed
             transform.position = Vector2.Lerp(startPoint, destination, time / 1); // lerp from A to B in one second
             time += Time.deltaTime;
             yield return null; //Wait for next frame
         }

         transform.position = destination;
         destReached = true;
    }
}
