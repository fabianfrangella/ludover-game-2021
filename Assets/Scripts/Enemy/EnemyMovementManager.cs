using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 1.0f;

    public float range;

    public float maxDistance;

    private EnemyHealthManager healthManager;

    private Animator animator;

    private Vector2 wayPoint;

    private Vector2 currentDirection;

    private Vector2 startPosition;

    private bool hasHitPlayer = false;

    private Vector2 prevLoc;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = gameObject.GetComponent<EnemyHealthManager>();
        animator = gameObject.GetComponent<Animator>();
        startPosition = transform.position;
        prevLoc = startPosition;
        SetNewDestination();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!healthManager.IsAlive() || hasHitPlayer)
        {
            StopMoving();
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
        SetAnimationDirection();
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
        hasHitPlayer = collision.collider.CompareTag(TagEnum.Player.ToString());
        if (hasHitPlayer)
        {
            return;
        }
        SetNewDestination();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hasHitPlayer = false;
        SetNewDestination();
    }
    private void StopMoving()
    {
        currentDirection = Vector2.zero;
        wayPoint = currentDirection;
    }

    private void SetNewDestination()
    {
        var minX = startPosition.x - maxDistance;
        var maxX = startPosition.x + maxDistance;
        var minY = startPosition.y - maxDistance;
        var maxY = startPosition.y + maxDistance;
        currentDirection = new Vector2(
            Random.Range(minX, maxX), 
            Random.Range(minY, maxY)
            );
        wayPoint = currentDirection;
    }

}
