﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public float speed = 2.0f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

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
       rb.velocity = DirectionWhereIsMoving().normalized * speed;
    }

    public Vector3 DirectionWhereIsMoving()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }

}
