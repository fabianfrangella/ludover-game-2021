using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationManager : EnemyAnimationManager
{

    public override void StartDieAnimation(float health)
    {
        animator.SetFloat("Health", health);
    }

    public void SetIsIdle(bool isIdle)
    {
        animator.SetBool("isIdle", isIdle);
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
        
    }
}
