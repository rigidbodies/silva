using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour

{
    [SerializeField] private float horizontalVelocity = 2f;
    [SerializeField] private float verticalVelocity = 2f;
    [SerializeField] private float amplitude = 0.3f;
    private float gameTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // Make bird move in waves
        gameTime += Time.deltaTime;
        transform.position += new Vector3(horizontalVelocity, amplitude * Mathf.Sin(gameTime * verticalVelocity), 0 ) * Time.deltaTime;

        // Destroy GameObject if outside main camera right boundary
        if (transform.position.x > 350)
        {
            Destroy(this.gameObject);
        }
    }
}
