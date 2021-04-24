using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public float speed = 2.0f;
    public Rigidbody2D rb;

    private bool isFacingRight;

    private Vector2 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        baseScale = transform.localScale;
    }

    private void FixedUpdate()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

       var horizontal = Input.GetAxisRaw("Horizontal");
       rb.velocity = DirectionWhereIsMoving().normalized * speed;
       Flip(horizontal);
    }

    public Vector3 DirectionWhereIsMoving()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }

    private void Flip(float horizontal)
    {
        isFacingRight = isFacingRight ? horizontal > 0 || horizontal == 0 : horizontal > 0;
        if (!isFacingRight && transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-baseScale.x, transform.localScale.y);
        }
        if (isFacingRight && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(baseScale.x, transform.localScale.y);
        }
    }

}
