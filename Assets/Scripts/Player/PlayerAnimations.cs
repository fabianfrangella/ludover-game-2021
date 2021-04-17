using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        PlayRunAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        PlayAttackAnimation();
    }

    private void PlayAttackAnimation()
    {
        var isAttacking = Input.GetMouseButtonDown(0);
        animator.SetBool("isAttacking", isAttacking);
    }
    private void PlayRunAnimation()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        if (horizontal == 0 && vertical == 0)
        {
            animator.SetFloat("Speed", -1);
            return;
        }
        if (Mathf.Abs(vertical) > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(vertical));
            return;
        }
        if (Mathf.Abs(horizontal) > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            return;
        }
    }
}
