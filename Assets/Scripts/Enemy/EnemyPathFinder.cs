using Audio;
using UnityEngine;
using Pathfinding;

public class EnemyPathFinder : MonoBehaviour
{

    public float speed = 1;
    public float nextWaypointDistance = 1f;
    public float lineOfSight = 4;
    public int currentWaypoint = 0;
    
    public Transform target;
    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator animator;
    
    private EnemyHealthManager healthManager;
    private bool hasReachedPlayer;
    
    private ObjectAudioArray audioManager;
    private int currentFootstep = 1;
    public State state = State.STILL;
    public Transform wanderer;
    public enum State
    {
        WANDERING,
        STILL
    }
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthManager = GetComponent<EnemyHealthManager>();
        audioManager = GetComponent<ObjectAudioArray>();
        target = null;
        if (state == State.WANDERING) target = wanderer;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        InvokeRepeating(nameof(PlayFootstep), 0f, 0.5f);
        animator.SetBool("isIdle", true);
    }
    
    private void PlayFootstep()
    {
        if (rb.velocity == Vector2.zero) return;
        audioManager.Play("FootstepSkeleton" + currentFootstep);
        currentFootstep++;
        if (currentFootstep > 5) currentFootstep = 1;
    }

    private void UpdatePath()
    {
       if (target == null || !seeker.IsDone() || !healthManager.IsAlive()) return;
       seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        currentWaypoint = 0;
    }
    private void Update()
    {
        if (state == State.STILL)
        {
            animator.SetBool("isIdle", rb.velocity == Vector2.zero);
            if (target == null || path == null)
            {
                SearchForTargetInArea();
                return;
            }
            
        }
        
        if (!healthManager.IsAlive())
        {
            StopMoving();
            return;
        }
        if (hasReachedPlayer) return;
        
        if (state == State.WANDERING)
        {
            if (path == null) return;
            if (target == wanderer)
            {
                SearchForTargetInArea();
            }
        }
        MoveTowardsWaypoint();
        SetNextWaypoint();
        CheckIfTargetIsTooFarAway();
        SetAnimationDirection();
    }
    
    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("isIdle", true);
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
        if (other.collider.CompareTag(TagEnum.Player.ToString()))
        {
            hasReachedPlayer = false;
        }
    }

    private void CheckIfTargetIsTooFarAway()
    {
        if (state == State.STILL)
        {
            if (target == null) return;
            if (Vector2.Distance(transform.position, target.position) > lineOfSight * 2)
            {
                StopMoving();
            }
        }
        if (state == State.WANDERING)
        {
            if (target != wanderer && Vector2.Distance(transform.position, target.position) > lineOfSight * 2) 
                target = wanderer;
        }
    }

    private void SearchForTargetInArea()
    {
        if (hasReachedPlayer) return;
        var collisions = Physics2D.OverlapCircleAll(transform.position, lineOfSight);
        foreach (var col in collisions)
        {
            if (!col.CompareTag(TagEnum.Player.ToString())) continue;
            target = col.transform;
            break;
        }
    }

    private void SetNextWaypoint()
    {
        if (path.vectorPath.Count <= currentWaypoint) return;
        
        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void MoveTowardsWaypoint()
    {
        if (path.vectorPath.Count <= currentWaypoint) return;
        animator.SetBool("isIdle", false);
        var dir = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
        rb.velocity = dir * speed;
    }

    private void SetAnimationDirection()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
    }
    
}
