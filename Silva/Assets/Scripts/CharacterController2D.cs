using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour

{

    [SerializeField] private float jumpForce = 400f;                // Force added when the player jumps
    [SerializeField] private float movementSpeed = 3f;              // Movement speed of the player
    float horizontalMove;                                           // Horizontal movement of the player
    private bool isGrounded;                                        // Whether the player is on the gound and not jumping
    private bool facingRight = true;                                // Check if player is facing right
    [SerializeField] float leftBoundary = -8.7f;                    // Mimimum value of player x
    [SerializeField] float rightBoundary = 298f;                   // Maximum value of player x

    [SerializeField] Animator animator;

    private Rigidbody2D rigidB;
    private SpriteRenderer render;

    [SerializeField] int score = 0;
    [SerializeField] Text scoreText;

    private Vector3 initialPosition;
    [SerializeField] float bottomBoundary = -5.0f;


    // Start is called before the first frame update
    void Start()
    {
        // Get RigidBody component of the player
        rigidB = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        scoreText.text = "Score: " + score;
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // Get horizontal movement and multiply with movement speed
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;

        // Apply absolute value of horizontalMove to the movement speed parameter of the animator to start movement animation
        animator.SetFloat("Movement Speed", Mathf.Abs(horizontalMove));


        // The player can only move if he is within the level boundaries
        if((transform.position.x > leftBoundary || horizontalMove > 0) 
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

        // If the player falls off he'll start the level from scratch
        if(this.transform.position.y < bottomBoundary)
        {
            Restart();
        }

        // If the player is looking opposite to the movement direction, the player image shoud be flipped
        if ((horizontalMove > 0 && !facingRight) || (horizontalMove < 0 && facingRight))
        {
            FlipPlayerImage();
        }
    }


    private void FlipPlayerImage()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Get the x scale property of the player and multiply it by -1 to flip the image
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void Restart()
    {
        //reposition player
        this.transform.position = initialPosition;

        //make sure he starts off again facing right
        if (!facingRight)
        {
            FlipPlayerImage();
        }

        //reset score
        IncreaseScore(-score);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Make player move with MovingPlatform while on it
        if(collision.transform.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }

        // Interrupt jump animation if Player lands before the animation has finished and set variables accordingly
        if (collision.gameObject.tag == "Ground" || collision.transform.tag == "MovingPlatform" || collision.gameObject.tag == "Ceiling")
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }

        // Restart if player is hit by a crystal
        if(collision.transform.tag == "Crystal")
        {
            StartCoroutine(Blink(8));            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Make player move on his own when leaving MovingPlatform
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = null;  
        }

        // The player is not grounded anymore if there is no collision with the ground
        if (collision.gameObject.tag == "Ground" || collision.transform.tag == "MovingPlatform" || collision.gameObject.tag == "Ceiling")
        {
            isGrounded = false;
        }
    }


    // Increase score and set text to the new score value
    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    IEnumerator Blink(int reps)
    {
        // Make player blink
        for(int i = 0; i<reps; i++)
        {
            Color c = render.material.color;
            c.a = 0.1f;
            render.material.color = c;
            yield return new WaitForSeconds(0.02f);
            c.a = 1.0f;
            render.material.color = c;
        }

        yield return new WaitForSeconds(0.5f);

        // Let player restart
        Restart();
    }
}
