using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] public float horizontalVelocity = -150.0f;
    [SerializeField] public float verticalVelocity = 0.0f;
    private Rigidbody2D rigidB;
    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
        rigidB.AddForce(new Vector2(horizontalVelocity,verticalVelocity));

        screenHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (-transform.position.y > 2 * screenHeight)
        {
            Destroy(this.gameObject);
        }
    }
}
