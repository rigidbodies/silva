using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float speed = 3;
    [SerializeField] private Transform[] points;
    [SerializeField] private int destinationIndex = 0;    // Points to the index of the current destination in the points array

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
            // Next destinationIndex
            destinationIndex = (destinationIndex + 1) % points.Length;
            // Update currentDestination
            currentDestination = points[destinationIndex];
        }
    }
}
