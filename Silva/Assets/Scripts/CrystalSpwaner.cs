using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpwaner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private List<GameObject> crystalPrefabs = new List<GameObject>();
    [SerializeField] private float spawnTime = 3.0f;

    private float timeCounter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Add random value to spawnTime to give each crystalSpawner its individual spawnTime
        spawnTime += Random.Range(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter >= spawnTime)
        {
            // Reset timeCounter
            timeCounter = 0.0f;

            // Spawn random crystal
            int crystalType = Random.Range(0, crystalPrefabs.Count);
            GameObject crystal = crystalPrefabs[crystalType];

            // Make sure there are crystals
            if (!crystal)
            {
                return;
            }

            // Instantiate crystal
            GameObject crystalInstance = Instantiate(crystal) as GameObject;
            crystalInstance.transform.localPosition = spawnPosition.position;
        }
        
    }
}
