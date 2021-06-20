using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    [SerializeField] Sprite checkpointNotReached;
    [SerializeField] Sprite checkpointReached;
    private SpriteRenderer spriteRenderer;
    public bool isCheckpointReached = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isCheckpointReached = true;
            spriteRenderer.sprite = checkpointReached;

        }
    }
}
