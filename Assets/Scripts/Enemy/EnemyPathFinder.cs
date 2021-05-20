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
    private Path path;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Vector2 prevLoc;
    private Animator animator;
    
    private EnemyHealthManager healthManager;
    
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        prevLoc = transform.position;
        healthManager = gameObject.GetComponent<EnemyHealthManager>();
        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }

    private void UpdatePath()
    {
        if (target == null || !seeker.IsDone()) return;
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
        if (!healthManager.IsAlive()) return;
        if (target == null)
        {
            SearchForTargetInArea();
            return;
        };
        if (path == null) return;

        reachedEndOfPath = currentWaypoint >= path.vectorPath.Count;

        if (reachedEndOfPath)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        MoveTowardsWaypoint();
        SetNextWaypoint();
        SetAnimationDirection();
        CheckIfTargetIsTooFarAway();
    }

    private void CheckIfTargetIsTooFarAway()
    {
        if (Vector2.Distance(transform.position, target.position) > lineOfSight * 2)
        {
            target = null;
            rb.velocity = Vector2.zero;
        }
    }

    private void SearchForTargetInArea()
    {
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
        var dir = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed);
        rb.velocity = (dir - (Vector2) transform.position).normalized * speed;
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
}
