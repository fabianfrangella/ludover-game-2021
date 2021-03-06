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

    private bool isDead = false;
    
    private void Start()
    {
        health = maxHealth;
        cl = GetComponent<CircleCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();
        // Workaround horrible para el audio, a futuro hay que manejar esto para cada tipo de enemigo en un script aparte
        if (IsSkeleton()) randomAudio = GetComponent<ObjectAudioArrayRandom>();
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
        if (IsAlive()) audioManager.Play("BodyHit");
        var finalDamage = absorption >= damage ? 0 : damage - absorption;
        health -= finalDamage;
        if (IsAlive())
        {
            if (IsSkeleton()) randomAudio.Play();
            return 0;
        }
        if (!isDead)
        {
            OnDeath?.Invoke();
            if (IsSkeleton()) audioManager.Play("SkeletonDeath");
            isDead = true;
            return experience;
        }
        return 0;
    }
    
    //Workaround horrible
    private bool IsSkeleton()
    {
        return gameObject.name[0].Equals('S');
    }
    public void SetAbsorption(float val)
    {
        absorption = val;
    }
    
}
