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
        spawnTime += Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= spawnTime)
        {
            timeCounter = 0f;
            if (!pagePrefab)
            {
                return;
            }

            GameObject pageInstance = Instantiate(pagePrefab) as GameObject;
            float horVel = Random.Range(0f, 30f);
            if (horVel <= 15f)
            {
                horVel -= 30;
            }
            PageController pageControllerScript = pageInstance.GetComponent<PageController>();
            pageControllerScript.horizontalVelocity = horVel;

            pageInstance.transform.localPosition = spawnPosition.position;
        }
    }
}
