using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float cameraDistance = 50.0f;

    //editable Gizmo boundaries
    //these boundaries are easy to adjust in the editor as they indicate the edge boundaries of the camera
    [SerializeField] private float topBoundary = 8.0f;
    [SerializeField] private float bottomBoundary = -8.0f;
    [SerializeField] private float leftBoundary = -8.7f;
    [SerializeField] private float rightBoundary = 100.0f;

    //computed boundaries
    //these boundaries are the actual boundaries of the camera as they handle the center boundaries of the camera
    private float topBoundaryC;
    private float bottomBoundaryC;
    private float leftBoundaryC;
    private float rightBoundaryC;

    // Awake is called before any Start function
    void Awake()
    {
        Camera.main.orthographicSize = ((Screen.height / 2) / cameraDistance);

        //compute camera center boundaries
        topBoundaryC = topBoundary - Camera.main.orthographicSize;
        bottomBoundaryC = bottomBoundary + Camera.main.orthographicSize;
        leftBoundaryC = leftBoundary + Camera.main.aspect * Camera.main.orthographicSize;
        rightBoundaryC = rightBoundary - Camera.main.aspect * Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        //let camera follow player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        //confine camera movement
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBoundaryC, rightBoundaryC),
            Mathf.Clamp(transform.position.y, bottomBoundaryC, topBoundaryC), 
            transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        //visualize camera boundaries in editor
        Gizmos.DrawLine(new Vector2(leftBoundary, bottomBoundary), new Vector2(leftBoundary, topBoundary));
        Gizmos.DrawLine(new Vector2(leftBoundary, topBoundary), new Vector2(rightBoundary, topBoundary));
        Gizmos.DrawLine(new Vector2(rightBoundary, topBoundary), new Vector2(rightBoundary, bottomBoundary));
        Gizmos.DrawLine(new Vector2(rightBoundary, bottomBoundary), new Vector2(leftBoundary, bottomBoundary));
    }



}
