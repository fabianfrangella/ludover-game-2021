using UnityEngine;

public class PlayerSpellManager : MonoBehaviour, PlayerAttackState
{
    public Shock shockSpellPrefab;

    public float castRate = 5f;
    float nextCastTime = 0f;

    private PlayerAnimationsManager playerAnimationsManager;

    void Start()
    {
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
    }

    public void HandleAttack()
    {
        if (Time.time >= nextCastTime)
        {
            playerAnimationsManager.PlayCastAnimation();
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
