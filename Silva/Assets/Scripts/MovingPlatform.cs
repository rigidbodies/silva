using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public float speed = 3;
    public Transform[] points;
    public int destinationIndex = 0; //points to the index of the current destination in the points array

    private Transform currentDestination; 

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = points[destinationIndex];
    }

    // Update is called once per frame
    void Update()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentDestination.position, Time.deltaTime * speed);
        
        if(platform.transform.position == currentDestination.position)
        {
            //next destination index
            destinationIndex = (destinationIndex + 1) % points.Length;
            //update current destination
            currentDestination = points[destinationIndex];
        }
    }
}
