using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float speed = 3;
    [SerializeField] Transform[] points;                    // enemy moves between 2 points

    [SerializeField] float attackTime = 3.0f;
    [SerializeField] Transform fireballSpanwPos;
    [SerializeField] GameObject fireball;
    [SerializeField] float fireballOffset = 1f;             // distance between the enemy and the spawn position of the fireball
    [SerializeField] float attackDelay = 2.0f;              // time to wait before instantiating fireball

    private int destinationIndex = 0;                       //points to the index of the current destination in the points array
    private bool facingLeft = true;
    private float timeCounter = 0.0f;
    private Transform currentDestination;

    private Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        currentDestination = points[destinationIndex];
        attackTime += Random.Range(0.0f, 1.0f);
        animator = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the enemy is facing left but the next destination is right of the player
        // or if the enemy is facing right but the next destination is left from the player
        if (facingLeft && currentDestination.position.x > enemy.transform.position.x
            || !facingLeft && currentDestination.position.x < enemy.transform.position.x)
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

        timeCounter += Time.deltaTime;

        if (timeCounter >= attackTime)
        {
            animator.SetBool("AttackTime", true);
            //reset timeCounter
            timeCounter = 0.0f;

            StartCoroutine(Attack());

        } else {
            animator.SetBool("AttackTime", false);
        }
    }

    public IEnumerator Attack()
    {
        // wait for some time so that the fireball is created at the end of the attacking animation and not immediately
        yield return new WaitForSeconds(attackDelay);

        //Instantiate fireball
        Fireball ball = Fireball.Instantiate(fireball, fireballSpanwPos.position, fireballSpanwPos.rotation).GetComponent<Fireball>();

        // if the enemy is facing left, the fireball should move left too
        if (facingLeft)
        {
            ball.direction = -1;
            // let fireball appear a bit more left than the enemy
            ball.transform.position += new Vector3(-fireballOffset, 0, 0);
        }
        // if the player is facing right, the fireball shoud move right too
        else
        {
            ball.direction = 1;
            // let fireball appear a bit more right than the enemy
            ball.transform.position += new Vector3(fireballOffset, 0, 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Make enemy move with MovingPlatform while on it
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }
}
