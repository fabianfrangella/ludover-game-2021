using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAnimationManager : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    public abstract void StartDieAnimation(float health);

    public abstract void PlayAttackAnimation();
    
}
