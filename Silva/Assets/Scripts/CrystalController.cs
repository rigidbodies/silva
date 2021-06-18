using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rigidBody;
    private SpriteRenderer render;
    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        render = transform.GetComponent<SpriteRenderer>();
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
        if(collision.transform.tag == "Ground")
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        //TODO
        yield return new WaitForSeconds(0.05f);
        Destroy(this.gameObject);
    }
}
