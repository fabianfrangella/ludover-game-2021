using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public HealthBar healthBar;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerStats = GetComponent<PlayerStats>();
        healthBar.SetMaxHealth(playerStats.maxHealth);
    }

    // Update is called once per frame
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
            playerAnimationsManager.PlayDeathAnimation();
            
        }
    }

    public void OnDamageReceived(int damage)
    {
        playerStats.health -= damage;
    }

    public void OnHealing(int healing)
    {
        if (playerStats.health + healing >= playerStats.maxHealth)
        {
            playerStats.health = playerStats.maxHealth;
            return;
        }
        playerStats.health += healing;
    }

}
