﻿using UnityEngine;

public class Shock : MonoBehaviour
{
    public Vector2 direction;

    public Animator animator;

    public float speed;
    public float distance;

    private Vector2 prevLoc;

    private void Start()
    {
        prevLoc = transform.position;
    }

    void FixedUpdate()
    {
        if (direction != null)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
        SetAnimationDirection();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void SetAnimationDirection()
    {
        var dir = (Vector2) transform.position - prevLoc;
        prevLoc = transform.position;
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
    }
}
