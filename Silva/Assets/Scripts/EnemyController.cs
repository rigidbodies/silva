using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemy;
    public float speed = 3;
    public Transform[] points;
    public int destinationIndex = 0; //points to the index of the current destination in the points array
    private bool facingLeft = true;

    private Transform currentDestination;

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = points[destinationIndex];

    }

    // Update is called once per frame
    void Update()
    {
        // if the enemy is facing left but the next destination is right of the player
        if (facingLeft && currentDestination.position.x > enemy.transform.position.x)
        {
            FlipPlayerImage();
        }
        // if the enemy is facing right but the next destination is left from the player
        else if (!facingLeft && currentDestination.position.x < enemy.transform.position.x)
        {
            FlipPlayerImage();
        }

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, currentDestination.position, Time.deltaTime * speed);

        if (enemy.transform.position == currentDestination.position)
        {
            //next destination index
            destinationIndex = (destinationIndex + 1) % points.Length;
            //update current destination
            currentDestination = points[destinationIndex];
        }
    }

    private void FlipPlayerImage()
    {
        // Switch the way the player is labelled as facing.
        facingLeft = !facingLeft;

        // Get the x scale property of the enemy and multiply it by -1 to flip the image
        Vector3 enemyScale = enemy.transform.localScale;
        enemyScale.x *= -1;
        enemy.transform.localScale = enemyScale;
    }
}
