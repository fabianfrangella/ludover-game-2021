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

    }

    public void PlayAttackAnimation()
    {
        animator.SetBool("isAttacking", true);
       
    }

    public void StopAttackAnimation()
    {
        animator.SetBool("isAttacking", false);
    }

    private void PlayRunAnimation()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }
}
