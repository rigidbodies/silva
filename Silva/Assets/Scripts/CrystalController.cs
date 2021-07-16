using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private float screenHeight;

    private Rigidbody2D rigidBody;
    private SpriteRenderer render;
    private AudioSource shatteringCrystals;


    // Start is called before the first frame update
    void Start()
    {
        // Get gameObject components
        rigidBody = transform.GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        shatteringCrystals = GetComponent<AudioSource>();

        // Initialise private variables
        rigidBody.velocity = new Vector2(0, -speed);
        screenHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy crystal if below main camera visibility
        if (- transform.position.y > 2 * screenHeight)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground" || collision.transform.tag == "Player" || collision.transform.tag == "MovingPlatform")
        {
            if (!shatteringCrystals.isPlaying)
            {
                shatteringCrystals.Play();
            }
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        // Let crystal fade out
        for(float f = 1; f >= 0.0; f -= 0.05f)
        {
            Color c = render.material.color;
            c.a = f;
            render.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        // Destroy crystal
        Destroy(this.gameObject);
    }
}
