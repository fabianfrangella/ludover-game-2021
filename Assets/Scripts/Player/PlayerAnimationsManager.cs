using System.Collections;
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
        SetIdleAnimation();
        PlayRunAnimation();
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
