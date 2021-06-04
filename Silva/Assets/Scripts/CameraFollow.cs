using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float cameraDistance = 50.0f;

    // Awake is called before any Start function
    void Awake()
    {
        Camera.main.orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }



}
