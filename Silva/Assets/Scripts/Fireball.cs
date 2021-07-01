using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] float speed;
    public int direction { get; set; }      // direction of fireball movement (1 for right, -1 for left)
    private float screenWidth;
    private float cameraRange;

    // Start is called before the first frame update
    void Start()
    {
        // get width of the screen
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    
    // destroy fireball if player is hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // let fireball move in a direction with given speed
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime * direction;

        // get actual range of camera-x value
        //cameraRange = screenWidth + Camera.main.transform.position.x;

        // if fireball is not visible on the screen anymore, destroy fireball
        if (transform.position.x > screenWidth + Camera.main.transform.position.x 
            || transform.position.x < -screenWidth + Camera.main.transform.position.x)
        {
            //this.transform.position = startingPosition;
            Destroy(gameObject);
        }
    }
}
