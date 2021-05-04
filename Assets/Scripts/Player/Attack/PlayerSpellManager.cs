using UnityEngine;

public class PlayerSpellManager : MonoBehaviour, PlayerAttackState
{
    public Shock shockSpellPrefab;

    public float castRate = 5f;
    float nextCastTime = 0f;

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
    }

    public void HandleAttack()
    {
        if (CanCast())
        {
            playerAnimationsManager.PlayCastAnimation();
            playerManaManager.OnManaLost(40);
            var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
            shock.SetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            shock.SetExtraDamage(playerStats.spellDamage);
            SetNextCastTime();
        }
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
