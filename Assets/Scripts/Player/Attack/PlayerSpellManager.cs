using System;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour, PlayerAttackState
{
    public Shock shockSpellPrefab;
    public Shield shieldSpellPrefab;
    public Spell[] availableSpells;

    public float castRate = 5f;
    float nextCastTime = 0f;

    private Spell selectedSpell;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerHealthManager playerHealthManager;
    private PlayerManaManager playerManaManager;
    private PlayerStats playerStats;


    void Start()
    {
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
        playerManaManager = GetComponent<PlayerManaManager>();
        playerStats = GetComponent<PlayerStats>();
        selectedSpell = availableSpells[0];
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

    private void CastSelectedSpell()
    {
        if (selectedSpell.Equals(Spell.Shock))
        {
            CastShock();
        }
        if (selectedSpell.Equals(Spell.Shield))
        {
            CastShield();
        }
    }

    private void CastShock()
    {
        playerAnimationsManager.PlayCastAnimation();
        playerManaManager.OnManaLost(40);
        var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
        shock.SetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        shock.SetExtraDamage(playerStats.spellDamage);
        SetNextCastTime();
    }

    private void CastShield()
    {
        playerAnimationsManager.PlayCastAnimation();
        playerManaManager.OnManaLost(80);
        var shield = Instantiate(shieldSpellPrefab, transform.position, Quaternion.identity);
        shield.transform.parent = transform;
    }

    private void ChangeSpell()
    {
        if (Input.GetKeyDown(KeyCode.F1) && HasSpell(Spell.Shock))
        {
            selectedSpell = Spell.Shock;
            Debug.Log("Change to spell Shock");
        }
        if (Input.GetKeyDown(KeyCode.F2) && HasSpell(Spell.Shield))
        {
            selectedSpell = Spell.Shield;
            Debug.Log("Change to spell Shield");
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
    Shield
}