using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    public float speed = 1.0f;
    public float range;
    public float maxDistance;
    public float lineOfSight;

    private Transform target;
    private EnemyHealthManager healthManager;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 wayPoint;
    private Vector2 startPosition;
    private Vector2 prevLoc;

    private bool hasHitPlayer = false;
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

        if (isColliding) 
        {


        }

        HandleRadiusCollisions();
        if (!healthManager.IsAlive() || hasHitPlayer)
        {
            StopMoving();
            return;
        }
        
        if (target != null) {
            wayPoint = Vector2.MoveTowards(this.transform.position, target.position, speed);      
        }


        if (target == null && Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
        SetVelocity();


        SetAnimationDirection();
    }

    void HandleRadiusCollisions()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, lineOfSight);
        bool playerFound = false;
        foreach (var collider in collisions)
        {
            playerFound = SetPlayerTarget(playerFound, collider);
        }
        if (!playerFound)
        {
            target = null;
        }
    }

    private bool SetPlayerTarget(bool playerFound, Collider2D collider)
    {
        if (!playerFound)
        {
            playerFound = collider.CompareTag(TagEnum.Player.ToString());
            if (playerFound)
            {
                target = collider.transform;
            }
        }

        return playerFound;
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

    public Vector2 GetDirectionWhereIsLooking()
    {
        return (Vector2)transform.position - prevLoc;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Building"){
            isColliding = true;
        }
        hasHitPlayer = collision.collider.CompareTag(TagEnum.Player.ToString());
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
