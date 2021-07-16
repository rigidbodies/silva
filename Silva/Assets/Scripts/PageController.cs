using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] public float horizontalVelocity = -150.0f;
    [SerializeField] public float verticalVelocity = 0.0f;

    private float screenHeight;

    private Rigidbody2D rigidB;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;

        rigidB = GetComponent<Rigidbody2D>();
        rigidB.AddForce(new Vector2(horizontalVelocity,verticalVelocity));
    }


    // Update is called once per frame
    void Update()
    {
        // Destroy page if below main camera visibility
        if (-transform.position.y > 2 * screenHeight)
        {
            Destroy(this.gameObject);
        }
    }
}
