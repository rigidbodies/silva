using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 3.0f;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject pagePrefab;

    private float timeCounter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Add random value to spawnTime to give each pageSpawner its individual spawnTime
        spawnTime += Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= spawnTime)
        {
            // Reset timeCounter
            timeCounter = 0f;

            // Make sure there is a pagePrefab
            if (!pagePrefab)
            {
                return;
            }

            // Instantiate page
            GameObject pageInstance = Instantiate(pagePrefab) as GameObject;

            // Compute random horizontal velocity in range (-30, -15) and (15, 30)
            float horVel = Random.Range(0f, 30f);
            if (horVel <= 15f)
            {
                horVel -= 30;
            }
            PageController pageControllerScript = pageInstance.GetComponent<PageController>();
            pageControllerScript.horizontalVelocity = horVel;

            // Place page in scene
            pageInstance.transform.localPosition = spawnPosition.position;
        }
    }
}
