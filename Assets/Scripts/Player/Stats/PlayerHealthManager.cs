using Audio;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public HealthBar healthBar;
    private PlayerAnimationsManager playerAnimationsManager;
    private Rigidbody2D rb;
    private AudioManager audioManager;
    private bool isDead = false;
    public event System.Action OnPlayerDeath;
    public event System.Action OnHitReceived;

    public float damageAbsorption;
    void Start()
    {
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        rb = GetComponent<Rigidbody2D>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(PlayerStats.instance.maxHealth);
        } 
        damageAbsorption = 0;
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        if (isDead) return;
        
        CheckDeath();
        if (healthBar != null)
        {
            SetHealthBar();
        }
        
    }

    public void SetHealthBar()
    {
        healthBar.SetHealth(PlayerStats.instance.health);
        healthBar.SetMaxHealth(PlayerStats.instance.maxHealth);
    }
    public bool IsAlive()
    {
        return PlayerStats.instance.health > 0;
    }

    void CheckDeath()
    {
        if (PlayerStats.instance.health <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            playerAnimationsManager.PlayDeathAnimation();
            isDead = true;
            if (OnPlayerDeath != null)
            {
                Debug.Log("Player died");
                OnPlayerDeath();
            }
        }
    }

    public void OnDamageReceived(float damage)
    {
        if (OnHitReceived != null) OnHitReceived();
        audioManager.Play("BodyHit");
        var finalDamage = damageAbsorption >= damage ? 0 : damage - damageAbsorption;
        PlayerStats.instance.health -= finalDamage;
    }

    public void AddDamageAbsorption(float abs)
    {
        damageAbsorption += abs;
    }

    public void SubstractDamageAbsorption(float abs)
    {
        damageAbsorption -= abs;
    }

    public void OnHealing(float healing)
    {
        if (PlayerStats.instance.health + healing >= PlayerStats.instance.maxHealth)
        {
            PlayerStats.instance.health = PlayerStats.instance.maxHealth;
            return;
        }
        PlayerStats.instance.health += healing;
    }

}
