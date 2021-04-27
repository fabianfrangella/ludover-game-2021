﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    // public solo para debugear
    public int health;
    public int maxHealth;

    public HealthBar healthBar;
    private PlayerAnimationsManager playerAnimationsManager;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerAnimationsManager = gameObject.GetComponent<PlayerAnimationsManager>();
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDeath();
        SetHealthBar();
    }

    public void SetHealthBar()
    {
        healthBar.SetHealth(health);
        healthBar.SetMaxHealth(maxHealth);
    }
    public bool IsAlive()
    {
        return health > 0;
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            playerAnimationsManager.SetDeathAnimation();
            
        }
    }

    public void OnDamageReceived(int damage)
    {
        health -= damage;
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
