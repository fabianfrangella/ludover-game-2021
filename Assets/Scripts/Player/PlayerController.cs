using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int health;
    int mana;
    int stamina;
    public int maxHealth;
    public int maxMana;
    public int maxStamina;
    public event System.Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        this.health = maxHealth;
        this.mana = maxMana;
        this.stamina = maxStamina;
    }

    private void FixedUpdate()
    {
        CheckLife();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckLife()
    {
        if (health <= 0)
        {
            OnPlayerDeath();
        }
    }

    void OnDamageReceived(int damage)
    {
        this.health -= damage;
    }

    void OnHealing(int healing)
    {
        if (this.health + healing >= this.maxHealth)
        {
            this.health = maxHealth;
            return;
        }
        this.health += healing;
    }

    void OnManaLost(int mana)
    {
        this.mana -= mana;
    }

    void OnManaReceived(int mana)
    {
        if (this.mana + mana >= this.maxMana)
        {
            this.mana = maxMana;
            return;
        }
        this.mana += mana;
    }
    void OnStaminaLost(int stamina)
    {
        this.stamina -= stamina;
    }

    void OnStaminaReceived(int stamina)
    {
        if (this.stamina + stamina >= this.maxStamina)
        {
            this.stamina = this.maxStamina;
            return;
        }
        this.stamina += stamina;
    }

}
