using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCollect : MonoBehaviour
{

    [SerializeField] private float floatValue = 2f;         // A value that is used to create a floating movement along the y-axis
    [SerializeField] private int scoreValue = 10;           // Value to be added to the player score every time a book is collected
    [SerializeField] private bool isFinalBook = false;

    private float originalY;                                // Variable for storing the original value of the y-position when the game starts

    private AudioSource bookSound;
    private SpriteRenderer spriteRenderer;

    private GameTriggeredMenuController levelCompletedMenu;
    private CharacterController2D character;


    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;

        // Get components of GameObject
        bookSound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get access to other scripts
        levelCompletedMenu = FindObjectOfType<GameTriggeredMenuController>();
        character = FindObjectOfType<CharacterController2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // Transform y-position to create a floating movement
        float shiftY = originalY + (Mathf.Sin(Time.time) * floatValue);
        transform.position = new Vector3(transform.position.x, shiftY, transform.position.z);
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
                Destroy(gameObject, 0.7f);
            }
            else
            {
                StartCoroutine(LevelCompleted());
            }
            character.IncreaseScore(scoreValue);
        }
    }


    IEnumerator LevelCompleted()
    {
        // Disable player movement
        character.canMove = false;

        // Wait until level completed sound has played
        yield return new WaitForSeconds(3.0f);

        // Display levelCompletedMenu + destroy final book
        levelCompletedMenu.DisplayMenu("levelCompletedMenu");
        Destroy(gameObject);
    }
}
