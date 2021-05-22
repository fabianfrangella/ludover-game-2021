using System;
using Audio;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour, PlayerAttackState
{
    public Shock shockSpellPrefab;
    public Shield shieldSpellPrefab;
    public Healing healingSpellPrefab;
    public Spell[] availableSpells;

    public float castRate = 5f;
    float nextCastTime = 0f;

    private bool isBuffActive;
    private Spell selectedSpell;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerHealthManager playerHealthManager;
    private PlayerManaManager playerManaManager;
    private PlayerStats playerStats;
    private AudioManager audioManager;


    void Start()
    {
        isBuffActive = false;
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
        playerManaManager = GetComponent<PlayerManaManager>();
        playerStats = GetComponent<PlayerStats>();
        selectedSpell = availableSpells[0];
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        ChangeSpell();
    }

    public void HandleAttack()
    {
        if (CanCast())
        {
            CastSelectedSpell();
        }
    }

    public void SetBuffActive(bool isActive)
    {
        isBuffActive = isActive;
    }
    private void CastSelectedSpell()
    {
        if (selectedSpell.Equals(Spell.Shock))
        {
            CastShock();
        }
        if (selectedSpell.Equals(Spell.Shield) && !isBuffActive)
        {
            CastShield();
        }
        if (selectedSpell.Equals(Spell.Healing))
        {
            CastHealing();
        }
    }

    private void CastHealing()
    {
        playerAnimationsManager.PlayCastAnimation();
        Instantiate(healingSpellPrefab, transform.position, Quaternion.identity);
        playerManaManager.OnManaLost(50);
        playerStats.health += 40;
    }

    private void CastShock()
    {
        audioManager.Play("ShockSpell");
        playerAnimationsManager.PlayCastAnimation();
        playerManaManager.OnManaLost(40);
        var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
        shock.SetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        shock.SetExtraDamage(playerStats.spellDamage);
        SetNextCastTime();
    }

    private void CastShield()
    {
        audioManager.Play("ShieldSpell");
        isBuffActive = true;
        playerAnimationsManager.PlayCastAnimation();
        playerManaManager.OnManaLost(80);
        var shield = Instantiate(shieldSpellPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;
        shield.SetAbsorption();
        SetNextCastTime();
    }

    private void ChangeSpell()
    {
        if (Input.GetKeyDown(KeyCode.F1) && HasSpell(Spell.Shock))
        {
            selectedSpell = Spell.Shock;
        }
        if (Input.GetKeyDown(KeyCode.F2) && HasSpell(Spell.Shield))
        {
            selectedSpell = Spell.Shield;
        }
        if (Input.GetKeyDown(KeyCode.F3) && HasSpell(Spell.Healing))
        {
            selectedSpell = Spell.Healing;
        }
    }

    private bool HasSpell(Spell spell)
    {
        return Array.Find(availableSpells, s => s.Equals(spell)).Equals(spell);
    }

    private bool CanCast()
    {
        return playerManaManager.GetCurrentMana() > 40 && Time.time >= nextCastTime && playerHealthManager.IsAlive();
    }

    private void SetNextCastTime()
    {
        nextCastTime = Time.time + 1f / castRate;
    }

    public PlayerAttackState GetNextState()
    {
        playerAnimationsManager.SetIsUsingMagic(false);
        return GetComponent<PlayerMeleeAttackManager>();
    }
}

public enum Spell
{
    Default,
    Shock,
    Shield,
    Healing
}