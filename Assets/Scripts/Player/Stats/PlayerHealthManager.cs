using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public HealthBar healthBar;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerStats playerStats;
    private Rigidbody2D rb;

    void Start()
    {
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        healthBar.SetMaxHealth(playerStats.maxHealth);
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
        playerStats.health -= damage;
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
