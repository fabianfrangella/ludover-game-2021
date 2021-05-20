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
    private Vector2 directionWhereIsLooking;
    private Animator animator;
    
    private EnemyHealthManager healthManager;
    private bool hasReachedPlayer;
    
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        directionWhereIsLooking = transform.position;
        healthManager = gameObject.GetComponent<EnemyHealthManager>();
        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
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
        SetAnimationDirection();
        if (!healthManager.IsAlive())
        {
            StopMoving();
            return;
        }

        if (target == null)
        {
            SearchForTargetInArea();
            return;
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

        SetLastDirectionWhereIsLooking();
    }

    private void SetLastDirectionWhereIsLooking()
    {
        if (rb.velocity == Vector2.zero) return;
        directionWhereIsLooking = rb.velocity;
        
    }

    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(TagEnum.Player.ToString()))
        {
            StopMoving();
        }
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
        //var dir = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed);
        var dir = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
        var force = dir * speed * Time.deltaTime;
        rb.AddForce(force);
        //rb.velocity = (dir - (Vector2) transform.position).normalized * speed;
    }

    private void SetAnimationDirection()
    {
        animator.SetBool("isIdle", rb.velocity == Vector2.zero);
        animator.SetFloat("Horizontal", directionWhereIsLooking.x);
        animator.SetFloat("Vertical", directionWhereIsLooking.y);
    }
    
    public Vector2 GetDirectionWhereIsLooking()
    {
        return directionWhereIsLooking;
    }
}
