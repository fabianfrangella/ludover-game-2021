﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DEPRECADISIMO NO USAR BAJO NINGUNA CIRCUNSTANCIA AAAAAAAAAAAAAAAAAAAAA
public class EnemyWandererManager : MonoBehaviour
{
    public float speed = 1.0f;
    public float range = 2;
    public float maxDistance = 2;

    private Vector2 wayPoint;
    private Vector2 startPosition;
    private Rigidbody2D rb;
    
    private void Start()
    {
        startPosition = transform.position;
        speed = gameObject.transform.parent.GetComponent<EnemyPathFinder>().speed * 2;
        rb = GetComponent<Rigidbody2D>();
        SetNewDestination();
    }
    
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
        Move();
    }
    
    private void Move()
    {
        rb.velocity = (wayPoint - (Vector2)transform.position).normalized * speed;
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
    
}
