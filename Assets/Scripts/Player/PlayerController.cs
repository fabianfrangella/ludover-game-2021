using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int health;
    int mana;
    // Lo dejo solo public para visualizar mientras pruebo
    public int stamina;
    int damage;
    public int maxHealth;
    public int maxMana;
    public int maxStamina;
    public event System.Action OnPlayerDeath;
    public event System.Action OnAttacking;

    // Start is called before the first frame update
    void Start()
    {
        this.health = maxHealth;
        this.mana = maxMana;
        this.stamina = maxStamina;
        this.damage = 20;
    }

    private void FixedUpdate()
    {
        CheckLife();
        OnStaminaReceived(1); 
    }
    // Update is called once per frame
    void Update()
    {
        HandleAttack();
    }

    public bool CanAttack()
    {
        return stamina >= 40;
    }

    void HandleAttack() 
    {
        var isAttacking = Input.GetMouseButtonDown(0);
        if (isAttacking)
        {
            if (OnAttacking != null)
            {
                OnAttacking();
            }
            if (stamina >= 40)
            {
                OnStaminaLost(40);
            }
        }
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
        if (this.stamina - stamina < 0)
        {
            this.stamina = 0;
            return;
        }
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
