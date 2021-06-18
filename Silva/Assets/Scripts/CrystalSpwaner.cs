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

    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter >= spawnTime)
        {
            //reset timeCounter
            timeCounter = 0.0f;

            //spawn random crystal
            int crystalType = Random.Range(0, crystalPrefabs.Count);
            GameObject crystal = crystalPrefabs[crystalType];

            //make sure there are crystals
            if (!crystal)
            {
                return;
            }

            //instantiate crystal
            GameObject crystalInstance = Instantiate(crystal) as GameObject;
            crystalInstance.transform.localPosition = spawnPosition.position;
        }
        
    }
}
