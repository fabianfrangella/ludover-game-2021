using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    public Shock shockSpellPrefab;
    
    public void CastShockSpell(Vector2 direction)
    {
        Instantiate(shockSpellPrefab, transform.position, Quaternion.identity).SetDirection(direction);
    }
}
