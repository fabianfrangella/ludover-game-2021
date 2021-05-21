using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathFinder : MonoBehaviour
{

    public float speed = 1;
    public float nextWaypointDistance = 1f;
    public float lineOfSight = 4;
    public int currentWaypoint = 0;
    
    private Transform target;
    private Transform wanderer;
    private Path path;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator animator;
    
    private EnemyHealthManager healthManager;
    private bool hasReachedPlayer;
    
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthManager = gameObject.GetComponent<EnemyHealthManager>();
        wanderer = transform.GetChild(transform.childCount - 1);
        target = wanderer;
        InvokeRepeating(nameof(UpdatePath), 0f, 1f);
    }

    private void UpdatePath()
    {
        if (target == null || !seeker.IsDone() || hasReachedPlayer || !healthManager.IsAlive()) return;
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (target == null) target = wanderer;
        SetAnimationDirection();
        if (!healthManager.IsAlive())
        {
            StopMoving();
            return;
        }

        if (!target.CompareTag(TagEnum.Player.ToString()))
        {
            SearchForTargetInArea();
        };
        if (path == null) return;

        reachedEndOfPath = currentWaypoint >= path.vectorPath.Count || hasReachedPlayer;
        if (reachedEndOfPath)
        {
            return;
        }

        MoveTowardsWaypoint();
        SetNextWaypoint();
        CheckIfTargetIsTooFarAway();
    }
    

    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasReachedPlayer = collision.collider.CompareTag(TagEnum.Player.ToString());
        if (hasReachedPlayer)
        {
            StopMoving();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        hasReachedPlayer = !other.collider.CompareTag(TagEnum.Player.ToString());
    }

    private void CheckIfTargetIsTooFarAway()
    {
        if (Vector2.Distance(transform.position, target.position) > lineOfSight * 2)
        {
            target = null;
            StopMoving();
        }
    }

    private void SearchForTargetInArea()
    {
        if (hasReachedPlayer) return;
        var collisions = Physics2D.OverlapCircleAll(transform.position, lineOfSight);
        foreach (var col in collisions)
        {
            var playerFound = col.CompareTag(TagEnum.Player.ToString());
            if (playerFound)
            {
                target = col.transform;
                break;
            }
        }
    }

    private void SetNextWaypoint()
    {
        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }

    private void MoveTowardsWaypoint()
    {
        var dir = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.velocity = dir * speed;
    }

    private void SetAnimationDirection()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
    }
    
}
