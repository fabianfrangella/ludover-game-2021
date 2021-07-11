using System.Collections;
using System.Collections.Generic;
using Pause;
using UnityEngine;

public class PlayerAnimationsManager : MonoBehaviour
{
    public Animator animator;
    private PlayerHealthManager playerHealthManager;
    private float lastHorizontal;
    private float lastVertical;
    private bool isAlive;

    private void Start()
    {
        playerHealthManager = GetComponent<PlayerHealthManager>();
        isAlive = true;
    }
    private void Update()
    {
        if (!playerHealthManager.IsAlive() || PauseManager.IsGamePaused()) return;
        SetLastDirection();
        SetIdleAnimation();
        PlayRunAnimation();
    }

    private void SetLastDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 direction = Input.mousePosition;
            direction = Camera.main.ScreenToWorldPoint(direction);
            direction -= (Vector2) transform.position;
            lastHorizontal = direction.x;
            lastVertical = direction.y;
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
    }

    public void SetIsUsingMagic(bool isUsingMagic)
    {
        animator.SetBool("IsUsingMagic", isUsingMagic);
    }
    public void PlayDeathAnimation()
    {
        isAlive = false;
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
        if ((horizontal != 0 || vertical != 0) && isAlive)
        {
            lastHorizontal = horizontal;
            lastVertical = vertical;
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
        }
    }

}
