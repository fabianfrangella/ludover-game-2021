using System.Collections;
using Enemy;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    public float speed = 1.0f;
    public float range;
    public float lineOfSight;
    
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
        HandleRadiusCollisions();
        if (!healthManager.IsAlive()) GetComponent<CircleCollider2D>().isTrigger = true;
        if (!healthManager.IsAlive() || hasHitPlayer || seeker == null)
        {
            StopMoving();
            return;
        }

        if (HasReachedDirection())
        {
            nextDir = seeker.GetNextDirectionTowardsTarget();
        }
        //transform.Translate(Vector2.MoveTowards(transform.position, nextDir, speed).normalized);
        rb.velocity = (nextDir - (Vector2) transform.position).normalized * speed;
        SetAnimationDirection();
    }

    bool HasReachedDirection()
    {
        return Vector2.Distance(transform.position, nextDir) <= 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHitPlayer = collision.collider.CompareTag(TagEnum.Player.ToString());
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag(TagEnum.Player.ToString())) hasHitPlayer = false;
    }

    void HandleRadiusCollisions()
    {
        var collisions = Physics2D.OverlapCircleAll(transform.position, lineOfSight);
        var playerFound = false;
        foreach (var collider in collisions)
        {
            playerFound = SetPlayerTarget(playerFound, collider);
        }
        //if (!playerFound) seeker = null;
    }

    private bool SetPlayerTarget(bool playerFound, Collider2D collider)
    {
        if (!playerFound)
        {
            playerFound = collider.CompareTag(TagEnum.Player.ToString());
            if (playerFound)
            {
                SetPath(collider.transform);
            }
        }
        return playerFound;
    }

    private void SetPath(Transform target)
    {
        if (seeker != null) return;
        seeker = new Seeker(target, transform);
        StartCoroutine(nameof(GetNextDirection));
    }

    private IEnumerator GetNextDirection()
    {
        nextDir = seeker.GetNextDirectionTowardsTarget();
        yield return null;
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
