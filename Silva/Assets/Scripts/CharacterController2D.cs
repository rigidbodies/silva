using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour

{

    [SerializeField] private float jumpForce = 400f;                // Force added when the player jumps
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private float leftBoundary = -8.7f;            // Mimimum x value of player
    [SerializeField] private float rightBoundary = 298f;            // Maximum x value of player
    [SerializeField] private float bottomBoundary = -5.0f;          // Minimum y value of player
    [SerializeField] int score = 0;
    [SerializeField] Text scoreText;
    [SerializeField] Animator animator;

    public bool canMove = true;                                     // Used for disabling movement while respawning, accessed by other scripts

    private float horizontalMove;                                   // Computed horizontal movement of the player
    private bool isGrounded;                                        // Check whether the player is on the ground and not jumping
    private bool facingRight = true;                                // Check if player is facing right
    private bool isRespawning = false;                              // Make sure only one life can be lost at a time
    private int soundPlayed;                                        // Mistake sound cannot be played more than three times

    private Rigidbody2D rigidB;
    private SpriteRenderer render;
    private AudioSource playerSound;

    private LivesController hearts;
    private GameTriggeredMenuController gameOverMenu;


    // Start is called before the first frame update
    void Start()
    {
        // Get components of the player
        rigidB = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        playerSound = GetComponent<AudioSource>();

        // Initialise private variables
        scoreText.text = "Score: " + score;
        respawnPosition = this.transform.position;
        soundPlayed = 0;

        // Get access to other scripts
        hearts = FindObjectOfType<LivesController>();
        gameOverMenu = FindObjectOfType<GameTriggeredMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is unable to move stop movement animation and exit update function
        if (!canMove)
        {
            animator.SetFloat("Movement Speed", 0f);
            return;  
        }

        // Get horizontal movement and multiply with movement speed
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;

        // Apply absolute value of horizontalMove to the movement speed parameter of the animator to start movement animation
        animator.SetFloat("Movement Speed", Mathf.Abs(horizontalMove));


        // The player can only move if he is within the level boundaries
        if ((transform.position.x > leftBoundary || horizontalMove > 0)
            && (transform.position.x < rightBoundary || horizontalMove < 0))
        {
            // Transform actual horizontal position according to horizontal movement and time passed
            transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime;
        }

        // The player cannot jump multiple times while he is still jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Add vertical force to player if he should jump
            rigidB.AddForce(new Vector2(0, jumpForce));
            // Set animator parameter to true, so that the jump animation can start
            animator.SetBool("IsJumping", true);
        }

        // If the player falls off he'll start the level from latest checkpoint
        if (this.transform.position.y < bottomBoundary && !isRespawning)
        {
            Restart();
        }

        // If the player is looking opposite to the movement direction, the player image shoud be flipped
        if ((horizontalMove > 0 && !facingRight) || (horizontalMove < 0 && facingRight))
        {
            FlipPlayerImage();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Make player move with MovingPlatform while on it
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }

        // Interrupt jump animation if the player lands before the animation has finished and set variables accordingly
        if (collision.gameObject.tag == "Ground" || collision.transform.tag == "MovingPlatform" || collision.gameObject.tag == "Ceiling")
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }

        // Restart if player is hit by an obstacle
        if (collision.transform.tag == "Crystal" || collision.transform.tag == "Plant" || collision.transform.tag == "Enemy")
        {
            StartCoroutine(Blink(3));
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        // Make player move on his own when leaving MovingPlatform
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }

        // The player is not grounded anymore if there is no collision with the tiles
        if (collision.gameObject.tag == "Ground" || collision.transform.tag == "MovingPlatform" || collision.gameObject.tag == "Ceiling")
        {
            isGrounded = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sets the respawn position to the position of the checkpoint colliding with the player
        if (collision.gameObject.tag == "Checkpoint")
        {
            respawnPosition = collision.transform.position;
        }

        // Make player blink if hit by a fireball
        if (collision.gameObject.tag == "Fireball")
        {
            StartCoroutine(Blink(3));
        }
    }


    private void Restart()
    {
        StartCoroutine(RespawnDelay());
    }


    IEnumerator RespawnDelay()
    {
        isRespawning = true;
        canMove = false;

        // Play mistake sound
        if (!playerSound.isPlaying && soundPlayed<3)
        {
            soundPlayed++;
            playerSound.Play();
        }

        // If the restart method is called it means that the player has lost a life
        hearts.Lose1Life();

        // If no life is left the scene is reloaded so that all values (score, lives, collected books, checkpoints) are resetted
        if (hearts.lives < 1)
        {
            // Wait until sound has played
            yield return new WaitForSeconds(1.9f);
            // GameOver screen
            gameOverMenu.DisplayMenu("gameOverMenu");
        }
        else
        {
            // Make player image disappear
            render.enabled = false;
            // Reposition player
            this.transform.position = respawnPosition;

            // Wait until sound has played
            yield return new WaitForSeconds(1.9f);
            // Make player image reappear
            render.enabled = true;

            // Make sure he starts off again facing right
            if (!facingRight)
            {
                FlipPlayerImage();
            }

            canMove = true;
        }   
                
        isRespawning = false;
    }


    IEnumerator Blink(int reps)
    {
        // Diasable player movement
        canMove = false;

        // Play mistake sound
        if (!playerSound.isPlaying)
        {
            soundPlayed++;
            playerSound.Play();
        }

        // Make player blink
        for (int i = 0; i < reps; i++)
        {
            Color c = render.material.color;
            c.a = 0.1f;
            render.material.color = c;
            yield return new WaitForSeconds(0.1f);
            c.a = 1.0f;
            render.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }

        // Let player restart
        if (!isRespawning)
        {
            Restart();
        }
    }


    private void FlipPlayerImage()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Get the x scale property of the player and multiply it by -1 to flip the image
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }


    // Increase score and set text to the new score value
    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }


    public int GetScore()
    {
        return score;
    }
}
