using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsManager : MonoBehaviour
{
    public Animator animator;
    private float lastHorizontal;
    private float lastVertical;

    private void FixedUpdate()
    {
        SetIdleAnimation();
        PlayRunAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIsUsingMagic(bool isUsingMagic)
    {
        animator.SetBool("IsUsingMagic", isUsingMagic);
    }
    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Die");
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayCastAnimation()
    {
        animator.SetTrigger("Cast");
    }

    private void SetIdleAnimation()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var isIdle = horizontal == 0 && vertical == 0;
        if (isIdle)
        {
            animator.SetFloat("Horizontal", lastHorizontal);
            animator.SetFloat("Vertical", lastVertical);
        }
        animator.SetBool("IsIdle", isIdle);
    }

    private void PlayRunAnimation()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            lastHorizontal = horizontal;
            lastVertical = vertical;
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
        }
    }

}
