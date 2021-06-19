using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private Rigidbody2D rigidBody;
    private SpriteRenderer render;
    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        rigidBody.velocity = new Vector2(0, -speed);
        screenHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(- transform.position.y > 2 * screenHeight)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground" || collision.transform.tag == "Player" || collision.transform.tag == "MovingPlatform")
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        //let crystal fade out
        for(float f = 1; f >= 0.0; f -= 0.05f)
        {
            Color c = render.material.color;
            c.a = f;
            render.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        //destroy crystal
        Destroy(this.gameObject);
    }
}
