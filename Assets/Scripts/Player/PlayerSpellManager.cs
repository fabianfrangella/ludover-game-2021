using UnityEngine;

public class PlayerSpellManager : MonoBehaviour, PlayerAttackState
{
    public Shock shockSpellPrefab;

    public void HandleAttack()
    {
        var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
        shock.SetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public PlayerAttackState GetNextState()
    {
        return GetComponent<PlayerMeleeAttackManager>();
    }
}
