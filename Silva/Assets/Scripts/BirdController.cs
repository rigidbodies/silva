using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour

{
    [SerializeField] private float horizontalVelocity = 2f;
    [SerializeField] private float verticalVelocity = 2f;
    [SerializeField] private float amplitude = 0.3f;
    private float gameTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        transform.position += new Vector3(horizontalVelocity, amplitude * Mathf.Sin(gameTime * verticalVelocity), 0 ) * Time.deltaTime;
        
        if (transform.position.x > 350)
        {
            Destroy(this.gameObject);

        }

        
    }
}
