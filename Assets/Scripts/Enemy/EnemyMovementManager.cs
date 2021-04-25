using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = DirectionWhereIsMoving().normalized * speed;
    }

    private float GetMoveDirection()
    {
        return 1;
    }

    public Vector3 DirectionWhereIsMoving()
    {
        return new Vector2(1, 0);
    }
}
