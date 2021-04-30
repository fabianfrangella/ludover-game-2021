﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int maxHealth;
    public float experience;
    public EnemyAnimationManager enemyAnimationManager;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimationManager.StartDieAnimation(health);
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
    public float OnDamageReceived(int damage)
    {
        health -= damage;
        if (!IsAlive())
        {
            return experience;
        }
        return 0;
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
