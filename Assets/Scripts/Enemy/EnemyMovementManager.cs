using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
   public Transform target;

    public Rigidbody2D rb;

    public float speed = 1.0f;

    public float lineOfSight;

    public float range;

    public float maxDistance;

    private EnemyHealthManager healthManager;

    private Animator animator;

    private Vector2 wayPoint;

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
        rb = GetComponent<Rigidbody2D>();
        SetNewDestination();
       // player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

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

      /*  
        if (distanceFromPlayer < lineOfSight)
        {
            
           wayPoint = Vector2.MoveTowards(this.transform.position, player.position, speed);

        }
      */

        SetAnimationDirection();
    }

    

  private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Player")
        {
            target = collision.transform;
                

        }
        
    }

    private void OnTriggerExit(Collider other)
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

 

    /* private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    } 
    */
}
