using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCollect : MonoBehaviour
{

    private CharacterController2D character;
    [SerializeField] private float floatValue = 2f;         // A value that is used to create a floating movement along the y-axis
    private float originalY;                                // Variable for storing the original value of the y-position when the game starts
    [SerializeField] private int scoreValue = 10;           // Value to be added to the player score every time a book is collected
    [SerializeField] private bool isFinalBook = false;

    private AudioSource bookSound;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
        bookSound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get access to the CharacterController2D script
        character = FindObjectOfType<CharacterController2D>();
        FloatUpDown();
    }

    // OnTriggerEnter2D is called when there is a collision between a book and the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only the player should be able to collect books on collision (not the enemies)
        if(collision.tag == "Player")
        {
            // Start playing sound effect
            if (!bookSound.isPlaying)
            {
                bookSound.Play();
            }
            // Make Sprite invisible
            spriteRenderer.enabled = false;
            // Destroy GameObject after sound effect has played
            if (!isFinalBook)
            {
                Destroy(gameObject, 0.7f);                    // Destroys the gameObject to wich the script is attaced to (the book)
            }
            else
            {
                Destroy(gameObject, 3.0f);                    // Destroys the gameObject to wich the script is attaced to (the book)
            }
            character.IncreaseScore(scoreValue);
        }
    }

    // Transform y-position to create a floating movement
    void FloatUpDown()
    {
        float shiftY = originalY + (Mathf.Sin(Time.time) * floatValue);
        transform.position = new Vector3(transform.position.x, shiftY, transform.position.z);
    }
}
