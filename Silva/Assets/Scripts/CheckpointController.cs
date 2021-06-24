using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    [SerializeField] Sprite checkpointNotReached;
    [SerializeField] Sprite checkpointReached;
    private SpriteRenderer spriteRenderer;
    private AudioSource checkpointSound;
    public bool isCheckpointReached = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        checkpointSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!isCheckpointReached)
            {
                checkpointSound.Play();
            }
            isCheckpointReached = true;
            spriteRenderer.sprite = checkpointReached;
            

        }
    }
}
