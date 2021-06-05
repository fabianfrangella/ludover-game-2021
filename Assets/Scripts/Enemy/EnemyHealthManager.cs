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
    public event System.Action OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        cl = GetComponent<CircleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
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
        audioManager.Play("BodyHit");
        var finalDamage = absorption >= damage ? 0 : damage - absorption;
        health -= finalDamage;
        if (!IsAlive())
        {
            if (OnDeath != null) OnDeath();
            return experience;
        }
        return 0;
    }

    public void SetAbsorption(float val)
    {
        absorption = val;
    }

    public void OnHealing(int healing)
    {
        if (health + healing >= maxHealth)
        {
            health = maxHealth;
            return;
        }
        health += healing;
    }
}
