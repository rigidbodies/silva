using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawner : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private float spawnTime = 8.0f;

    private float timeCounter = 0.0f;
    private bool pointsCollected = false;

    private AudioSource extraPointSound;
    private CharacterController2D character;


    // Start is called before the first frame update
    void Start()
    {
        // Get AudioSource component of gameObject
        extraPointSound = GetComponent<AudioSource>();
        // Get access to CharacterController2D script
        character = FindObjectOfType<CharacterController2D>();

        // Make sure spawnTime isn't shorter than audio clip
        if (spawnTime < 3.5f)
        {
            spawnTime = 3.5f;
        }

        // Randomise spawnTime
        spawnTime += Random.Range(0.0f,3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter >= spawnTime)
        {
            // Reset timeCounter
            timeCounter = 0;

            if (!extraPointSound.isPlaying)
            {
                pointsCollected = false;
                extraPointSound.Play();
            }
        }

        bool enterPressed = Input.GetKeyDown(KeyCode.Return);

        if(extraPointSound.isPlaying && enterPressed && !pointsCollected)
        {
            // Points can be collected once per sound spawn
            pointsCollected = true;
            // Add collected points to player score
            character.IncreaseScore(scoreValue);
        }
    }
}
