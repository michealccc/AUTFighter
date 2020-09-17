using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraPos;
    public Transform p1Pos;
    public Transform p2Pos;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Camera width: " + cam.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPos();
    }

    private void SetCameraPos()
    {
        if(Mathf.Abs((p2Pos.position.x - p1Pos.position.x)) >= cam.orthographicSize)        //Not near each other
        {
            if(Mathf.Abs((p2Pos.position.x - p1Pos.position.x)) < cam.orthographicSize * 2)         //Full screen limit
            {
                float camXPos = p1Pos.position.x + (p2Pos.position.x - p1Pos.position.x) / 2;
                cameraPos.position = new Vector2(camXPos, cameraPos.position.y);
            }
        }
    }
}
