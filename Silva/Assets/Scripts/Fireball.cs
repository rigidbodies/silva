using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] float speed;                       // Width of fireball curve
    [SerializeField] private float amplitude;           // Height of fireball curve
    [SerializeField] AudioSource creationSound;
    [SerializeField] AudioSource landingSound;
    public int direction { get; set; }                  // Direction of fireball movement (1 for right, -1 for left)

    private float screenWidth;
    private Rigidbody2D rigidB;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        // Get width of the screen
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Get components
        rigidB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Make fireball move in arched trajectory
        rigidB.AddForce(new Vector2(speed * direction, amplitude));

        creationSound.Play();
    }


    // Update is called once per frame
    void Update()
    {

        // If fireball is not visible on the screen anymore, destroy fireball
        if (transform.position.x > screenWidth * 1.5 + Camera.main.transform.position.x
            || transform.position.x < -screenWidth * 1.5 + Camera.main.transform.position.x)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make fireball move with MovingPlatform when landing on it
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }

        if (collision.transform.tag == "Player" || collision.transform.tag == "Ground" || collision.transform.tag == "Ceiling" || collision.transform.tag == "MovingPlatform")
        {
            // Stop moving
            rigidB.velocity = new Vector2(0, 0);
            rigidB.gravityScale = 0;
            // Make fireball fade out
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        landingSound.Play();

        // Let fireball fade out
        for (float f = 1; f >= 0.0; f -= 0.05f)
        {
            Color c = spriteRenderer.material.color;
            c.a = f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        // Destroy fireball
        Destroy(this.gameObject);
    }
}
