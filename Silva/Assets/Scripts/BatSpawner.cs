using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private float spawnTime = 5.0f;
    private float timeCounter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime += Random.Range(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter >= spawnTime)
        {
            timeCounter = 0.0f;

            // Make sure there is a batPrefab
            if (!batPrefab)
            {
                return;
            }        

            // Instantiate bat
            GameObject batInstance = Instantiate(batPrefab) as GameObject;

            // Set horizontal velocity of bat Instance
            float horVel = Random.Range(-2.0f, 2.0f);
            BatController batControllerScript = batInstance.GetComponent<BatController>();
            batControllerScript.horizontalVelocity = horVel;

            // Place batInstance on spawnPosition
            batInstance.transform.localPosition = spawnPosition.position;
        }
    }
}
