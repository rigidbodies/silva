using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private float spawnTime = 3f;
    private float timeCounter = 0f;

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= spawnTime)
        {
            timeCounter = 0f;

            // Make sure there is a birdPrefab
            if (!birdPrefab)
            {
                return;
            }
             
            // Instantiate bird
            GameObject birdInstance = Instantiate(birdPrefab) as GameObject;
            birdInstance.transform.localPosition = spawnPosition.position;
    
        }
    }
}
