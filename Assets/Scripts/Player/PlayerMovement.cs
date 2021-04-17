using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private bool isFacingRight;
    private Vector2 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        this.isFacingRight = true;
        this.baseScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var v3 = new Vector3(horizontal, vertical, 0);
        transform.Translate(speed * v3.normalized * Time.deltaTime);
        Flip(horizontal);
    }

    private void Flip(float horizontal)
    {
        isFacingRight = isFacingRight ? horizontal > 0 || horizontal == 0 : horizontal > 0;
        if (!isFacingRight && transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-baseScale.x, transform.localScale.y);
            return;

        }
        if (isFacingRight && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(baseScale.x, transform.localScale.y);
        }
    }

}
