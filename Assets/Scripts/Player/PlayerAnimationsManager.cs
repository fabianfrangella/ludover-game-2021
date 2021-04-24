﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsManager : MonoBehaviour
{
    public Animator animator;
    private float lastHorizontal;
    private float lastVertical;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        PlayRunAnimation();
        SetIdleAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
       
    }

    private void SetIdleAnimation()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var isIdle = horizontal == 0 && vertical == 0;
        animator.SetBool("IsIdle", isIdle);
        if (isIdle)
        {
            animator.SetFloat("Horizontal", lastHorizontal);
            animator.SetFloat("Vertical", lastVertical);
        }
    }

    private void PlayRunAnimation()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            lastHorizontal = horizontal;
            lastVertical = vertical;
            return;
        }
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }
}
