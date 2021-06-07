using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxHealth;
    public float experience;
    public EnemyAnimationManager enemyAnimationManager;

    private AudioManager audioManager;
    private CircleCollider2D cl;
    private float absorption = 0;
    public event Action OnDeath;

    public event Action OnHit;
    
    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        cl = GetComponent<CircleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!IsAlive())
        {
            cl.isTrigger = true;
        }
        enemyAnimationManager.StartDieAnimation(health);
    }

    private void OnDestroy()
    {
        var childC = transform.childCount;
        for (var i = 0; i < childC; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    /**
     * <summary>
     * Decrements the health value and if is dead returns the given experience, 0 otherwise
     * </summary>
     */
    public float OnDamageReceived(float damage)
    {
        OnHit?.Invoke();
        audioManager.Play("BodyHit");
        var finalDamage = absorption >= damage ? 0 : damage - absorption;
        health -= finalDamage;
        if (IsAlive()) return 0;
        Debug.Log("Skeleton died");
        OnDeath?.Invoke();
        return experience;
    }

    public void SetAbsorption(float val)
    {
        absorption = val;
    }
    
}
