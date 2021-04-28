using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    public Transform target;

    public Rigidbody2D rb;

    public float speed = 1.0f;

    public float range;

    public float maxDistance;

    private EnemyHealthManager healthManager;

    private Animator animator;

    private Vector2 wayPoint;

    private Vector2 startPosition;

    private bool hasHitPlayer = false;

    private Vector2 prevLoc;

    private bool isColliding = false;

   
   
    void Start()
    {
        healthManager = gameObject.GetComponent<EnemyHealthManager>();
        animator = gameObject.GetComponent<Animator>();
        startPosition = transform.position;
        prevLoc = startPosition;
        rb = GetComponent<Rigidbody2D>();
        SetNewDestination();
     

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log(isColliding);

        if (isColliding == true)
        {
            

        }

        if (!healthManager.IsAlive() || hasHitPlayer)
        {
            StopMoving();
            return;
        }
        SetVelocity();
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }

        if (target != null) {

            
           
            wayPoint = Vector2.MoveTowards(this.transform.position, target.position, speed);


           

        }

        SetAnimationDirection();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            target = other.transform;


        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (target != null)
        {

            target = null;
            

        }


    }

    private void SetVelocity()
    {
        rb.velocity = (wayPoint - (Vector2)transform.position).normalized * speed;
    }

    private void SetAnimationDirection()
    {
        var direction = (Vector2) transform.position - prevLoc;
        prevLoc = transform.position;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Building"){

            isColliding = true;
            
        }
        hasHitPlayer = collision.collider.CompareTag(TagEnum.Player.ToString());
        /* eventualmente esto sera algo como 
         * if (hasHitPlayer) {
         *  attack() 
         *  return;
         * }
         * ReturnToStartPosition();
         */
        if (!hasHitPlayer)
        {
            SetWayPointToStartPosition();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Building")
        {

            isColliding = false;

        }

        hasHitPlayer = false;
    }
    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private void SetNewDestination()
    {
        var minX = startPosition.x - maxDistance;
        var maxX = startPosition.x + maxDistance;
        var minY = startPosition.y - maxDistance;
        var maxY = startPosition.y + maxDistance;
        wayPoint = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
            );
    }

    private void SetWayPointToStartPosition()
    {
        wayPoint = startPosition;
    }

 

   
}
