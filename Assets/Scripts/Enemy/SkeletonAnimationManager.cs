using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationManager : EnemyAnimationManager
{
    public float timeToDisappearBody = 5;
    private float timeSinceDeath = 0;
    private bool isDead = false;
    public override void StartDieAnimation(float health)
    {
        isDead = health <= 0;
        animator.SetFloat("Health", health);
    }

    public void SetIsIdle(bool isIdle)
    {
        animator.SetBool("isIdle", isIdle);
    }

    override public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isIdle", false);
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        if (timeSinceDeath < timeToDisappearBody)
        {
            timeSinceDeath += Time.deltaTime;
        }
        if (timeSinceDeath > timeToDisappearBody)
        {
            gameObject.SetActive(false);
        }
    }
}
