using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public HealthBar healthBar;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerStats playerStats;
    private Rigidbody2D rb;

    private float damageAbsorption;
    void Start()
    {
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        healthBar.SetMaxHealth(playerStats.maxHealth);
        damageAbsorption = 0;
    }

    void FixedUpdate()
    {
        CheckDeath();
        SetHealthBar();
    }

    public void SetHealthBar()
    {
        healthBar.SetHealth(playerStats.health);
        healthBar.SetMaxHealth(playerStats.maxHealth);
    }
    public bool IsAlive()
    {
        return playerStats.health > 0;
    }

    void CheckDeath()
    {
        if (playerStats.health <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            playerAnimationsManager.PlayDeathAnimation();
        }
    }

    public void OnDamageReceived(float damage)
    {
        var finalDamage = damageAbsorption >= damage ? 0 : damage - damageAbsorption;
        playerStats.health -= finalDamage;
    }

    public void AddDamageAbsorption(float abs)
    {
        damageAbsorption += abs;
    }

    public void SubstractDamageAbsorption(float abs)
    {
        damageAbsorption -= abs;
        Debug.Log(damageAbsorption);
    }

    public void OnHealing(float healing)
    {
        if (playerStats.health + healing >= playerStats.maxHealth)
        {
            playerStats.health = playerStats.maxHealth;
            return;
        }
        playerStats.health += healing;
    }

}
