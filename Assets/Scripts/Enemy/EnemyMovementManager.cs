using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 2.0f;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var direction = DirectionWhereIsMoving();
        rb.velocity = direction.normalized * speed;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    public Vector3 DirectionWhereIsMoving()
    {
        return new Vector2(1, 0);
    }
}
