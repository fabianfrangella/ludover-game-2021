using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
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
    private Vector2 startPosition;
    private Vector2 prevLoc;
    
    private Seeker seeker;
    private Vector2 nextDir;

    private bool hasHitPlayer;
    
    
    void Start()
    {
        healthManager = gameObject.GetComponent<EnemyHealthManager>();
        animator = gameObject.GetComponent<Animator>();
        startPosition = transform.position;
        prevLoc = startPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!healthManager.IsAlive())
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        /*
        HandleRadiusCollisions();
        // TODO llevar esto a otro lado (ver si puede ser mas lindo)
        // la idea de este if es que cuando se muera, puedas pasar por arriba del chobi

        // TODO llevar esto a otro metodo
        if (!healthManager.IsAlive() || hasHitPlayer)
        {
            StopMoving();
            return;
        }

        // TODO llevar esto a otro metodo
        if (target != null) {
            wayPoint = Vector2.MoveTowards(this.transform.position, target.position, speed);      
        }

        // TODO llevar esto a otro metodo
        if (target == null && Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
        SetVelocity();
        */
        HandleRadiusCollisions();
        if (target == null) return;
        if (seeker.HasFinishPath())
        {
            StopMoving();
            return;
        }
        nextDir = seeker.GetNextDirectionTowardsTarget();
        rb.velocity = (nextDir - (Vector2)transform.position).normalized * speed;
        SetAnimationDirection();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHitPlayer = collision.collider.CompareTag(TagEnum.Player.ToString());
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag(TagEnum.Player.ToString()))
        {
            hasHitPlayer = false;
        }
    }
    

    bool HasReachedDirection()
    {
        return Vector2.Distance(transform.position, nextDir) < range;
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
         //   target = null;
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
                seeker = new Seeker(target, transform);
            }
        }

        return playerFound;
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
    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }
}
