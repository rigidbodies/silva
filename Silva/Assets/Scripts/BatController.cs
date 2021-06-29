using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField] public float horizontalVelocity = 0.0f;
    [SerializeField] private float verticalVelocity = 2.0f;

    private float screenHeight;
    //private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        //startingPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move bat upwards
        transform.position += new Vector3(horizontalVelocity, verticalVelocity, 0) * Time.deltaTime;

        if (transform.position.y > 2 * screenHeight)
        {
            //this.transform.position = startingPosition;
            Destroy(this.gameObject);
        }
    }
}
