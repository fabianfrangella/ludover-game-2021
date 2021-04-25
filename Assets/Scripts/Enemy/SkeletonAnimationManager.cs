using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationManager : EnemyAnimationManager
{
    public override void StartDieAnimation(float health)
    {
        animator.SetFloat("Health", health);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
