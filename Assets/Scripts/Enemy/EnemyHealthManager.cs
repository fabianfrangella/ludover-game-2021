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
    public HealthBar healthBar;
    
    private AudioManager audioManager;
    private ObjectAudioArrayRandom randomAudio;
    private CircleCollider2D cl;
    private float absorption = 0;
    public event Action OnDeath;

    public event Action OnHit;
    
    private void Start()
    {
        health = maxHealth;
        cl = GetComponent<CircleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
        // Workaround horrible para el audio, a futuro hay que manejar esto para cada tipo de enemigo en un script aparte
        if (gameObject.name[0].Equals('S')) randomAudio = GetComponent<ObjectAudioArrayRandom>();
    }
    
    private void Update()
    {
        if (!IsAlive())
        {
            cl.isTrigger = true;
        }

        SetHealthBar();
        enemyAnimationManager.StartDieAnimation(health);
    }

    private void SetHealthBar()
    {
        healthBar.SetHealth(health);
        healthBar.SetMaxHealth(maxHealth);
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
        // Workaround horrible para el audio, a futuro hay que manejar esto para cada tipo de enemigo en un script aparte
        if (gameObject.name[0].Equals('S')) randomAudio.Play();
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
