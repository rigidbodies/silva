using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = 4.0f;
    [SerializeField] private float recoveringSpeed = 2.0f;

    private bool falling = false;
    private bool recovering = false;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make falling tile move downwards
        if (falling)
        {
            transform.position += new Vector3(0, -fallingSpeed, 0) * Time.deltaTime;
        }

        // Make falling tile move upwards until it has reached its originalPosition
        if (recovering && this.transform.position != originalPosition)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, originalPosition, Time.deltaTime * recoveringSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            recovering = false;
            falling = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            falling = false;
            recovering = true;
        }
    }
}
