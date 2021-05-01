using UnityEngine;

public class PlayerSpellManager : MonoBehaviour, PlayerAttackState
{
    public Shock shockSpellPrefab;

    public float castRate = 5f;
    float nextCastTime = 0f;

    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerManaManager playerManaManager;

    void Start()
    {
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerManaManager = GetComponent<PlayerManaManager>();
    }

    public void HandleAttack()
    {
        if (playerManaManager.GetCurrentMana() > 40 && Time.time >= nextCastTime)
        {
            playerAnimationsManager.PlayCastAnimation();
            playerManaManager.OnManaLost(40);
            var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
            shock.SetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            SetNextCastTime();
        }
    }

    private void SetNextCastTime()
    {
        nextCastTime = Time.time + 1f / castRate;
    }

    public PlayerAttackState GetNextState()
    {
        return GetComponent<PlayerMeleeAttackManager>();
    }
}
