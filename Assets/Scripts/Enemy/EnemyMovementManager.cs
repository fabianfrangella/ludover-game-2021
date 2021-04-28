using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    public Transform target; // este atributo deberia ser private
    public Rigidbody2D rb;
    // como regla de oro, los unicos atributos que ponemos public son los que queremos tocar desde el editor de unity 
    // o desde otros scripts (y esto es debatible tambien), sino va private
    public float speed = 1.0f;
    public float range;
    public float maxDistance;

    private EnemyHealthManager healthManager;
    private Animator animator;
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
        // el rb ya lo estamos metiendo a mano en la scene, no hace falta hacer un GetComponent
        // esto no esta necesariamente mal, pero si lo hacemos asi, pasemos el rb a private
        SetNewDestination();
     

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log(isColliding);

        if (isColliding == true) 
        {
        /* Tip para nico -> decir if (isColliding) es lo mismo que decir isColliding == true
         * isColliding es un boolean, por lo tanto sus valores son o true o false, 
         * el if espera tener algo que sea true o false, por lo tanto con decir if (isColliding) es suficiente
         * decir isColliding == true es lo mismo que decir true == true o false == true, es redundante
         * si isColliding es true, decir true == true es lo mismo que decir isColliding
         * y si isColliding es false, decir false == true es lo mismo que decir isColliding
         * Como regla general, si en algún lado estás comparando algo == true o algo == false está mal
         * y podes reducirlo simplemente a "algo", nunca de los jamases poner == true o == false
         */
        }

        if (!healthManager.IsAlive() || hasHitPlayer)
        {
            StopMoving();
            return;
        }
        
        if (target != null) {
            wayPoint = Vector2.MoveTowards(this.transform.position, target.position, speed);      
        }

        // aca te agregue la validacion de target == null, 
        // porque sino seteas un nuevo destino una vez que se acerco mucho al target
        if (target == null && Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
        SetVelocity();


        SetAnimationDirection();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Player")
        // acordate que es mejor usar el TagEnum para no dejar hardcodeado el tag
        if(other.CompareTag(TagEnum.Player.ToString()))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Aca te faltaba validar que el tag del other sea player
        //sino cuando triggereaba por tocar otra cosa, seteaba target null siempre
        if (other.CompareTag(TagEnum.Player.ToString()) && target != null)
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
        if(collision.gameObject.tag == "Building"){
            isColliding = true;
        }
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
        if (collision.gameObject.tag == "Building")
        {
            isColliding = false;
        }
        // esto te lo agrego porque cuando colisionas con el bicho, se triggerea tambien el OnTriggerExit
        // entonces para mantener el target, lo volvemos a setear, es medio un hackazo, pero por ahora va
        if (collision.collider.CompareTag(TagEnum.Player.ToString()))
        {
            target = collision.transform;
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
