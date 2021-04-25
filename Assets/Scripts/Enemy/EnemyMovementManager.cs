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

    private bool hasHitPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = gameObject.GetComponent<EnemyHealthManager>();
        animator = gameObject.GetComponent<Animator>();
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
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
        animator.SetFloat("Horizontal", currentDirection.x);
        animator.SetFloat("Vertical", currentDirection.y);
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
        currentDirection = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
        wayPoint = currentDirection;
    }

}
