using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsManager : MonoBehaviour
{
    public PlayerController controller;
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
        var isAttacking = Input.GetMouseButtonDown(0) && controller.CanAttack();
        animator.SetBool("isAttacking", isAttacking);
       
    }
    private void PlayRunAnimation()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }
}
