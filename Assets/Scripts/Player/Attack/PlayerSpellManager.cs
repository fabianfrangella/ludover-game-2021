using System;
using Audio;
using Hud;
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
    private AudioManager audioManager;
    private AttackMode ui;


    void Start()
    {
        isBuffActive = false;
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
        playerManaManager = GetComponent<PlayerManaManager>();
        selectedSpell = availableSpells[0];
        audioManager = FindObjectOfType<AudioManager>();
        ui = FindObjectOfType<AttackMode>();
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
        audioManager.Play("HealingSpell");
        playerAnimationsManager.PlayCastAnimation();
        Instantiate(healingSpellPrefab, transform.position, Quaternion.identity);
        playerManaManager.OnManaLost(50);
        PlayerStats.instance.health += 40;
    }

    private void CastShock()
    {
        audioManager.Play("ShockSpell");
        playerAnimationsManager.PlayCastAnimation();
        playerManaManager.OnManaLost(40);
        var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
        shock.SetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        shock.SetExtraDamage(PlayerStats.instance.spellDamage);
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && HasSpell(Spell.Shock))
        {
            audioManager.Play("ChangeSpell");
            selectedSpell = Spell.Shock;
            ui.SetSelectedSpell(Spell.Shock.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && HasSpell(Spell.Shield))
        {
            audioManager.Play("ChangeSpell");
            selectedSpell = Spell.Shield;
            ui.SetSelectedSpell(Spell.Shield.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && HasSpell(Spell.Healing))
        {
            audioManager.Play("ChangeSpell");
            selectedSpell = Spell.Healing;
            ui.SetSelectedSpell(Spell.Healing.ToString());
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
        FindObjectOfType<CursorManager>().SetMeleeCursor();
        ui.isUsingMagic = false;
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