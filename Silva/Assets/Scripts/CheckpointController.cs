using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    [SerializeField] Sprite checkpointNotReached;
    [SerializeField] Sprite checkpointReached;

    private bool isCheckpointReached = false;

    private SpriteRenderer spriteRenderer;
    private AudioSource checkpointSound;


    // Start is called before the first frame update
    void Start()
    {
        // Get gameObject components
        spriteRenderer = GetComponent<SpriteRenderer>();
        checkpointSound = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checkpoint is only reached when player collides with it
        if (collision.tag == "Player")
        {
            if (!isCheckpointReached)
            {
                checkpointSound.Play();
            }
            isCheckpointReached = true;

            // Exchange sprite
            spriteRenderer.sprite = checkpointReached;
        }
    }
}