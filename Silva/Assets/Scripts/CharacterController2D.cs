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

    private Rigidbody2D rigidB;

    [SerializeField] int score = 0;
    public Text scoreText;

    private Vector3 initialPosition;
    [SerializeField] float bottomBoundary = -5.0f;


    // Start is called before the first frame update
    void Start()
    {
        // Get RigidBody component of the player
        rigidB = GetComponent<Rigidbody2D>();
        //GameObject scoreObject = GameObject.Find("Score");
        //scoreText = scoreObject.GetComponent<Text>();
        scoreText.text = "Score: " + score;

        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // Get horizontal movement and multiply with movement speed
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;

        // Transform actual horizontal position according to horizontal movement and time passed
        transform.position += new Vector3(horizontalMove, 0, 0) * Time.deltaTime;

        // If there is no velocity on the y-axis, the player is not jumping
        isGrounded = Mathf.Abs(rigidB.velocity.y) < 0.001f;

        // The player cannot jump multiple times while he is still jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Add vertival force to player if he should jump
            rigidB.AddForce(new Vector2(0, jumpForce));
        }

        // If the player falls off he'll start the level from scratch
        if(this.transform.position.y < bottomBoundary)
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

    // Increase score and set text to the new score value
    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}
