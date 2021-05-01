using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    public Shock shockSpellPrefab;
    
    public void CastShockSpell(Vector2 direction)
    {
        var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
        shock.SetDirection(direction);
        shock.transform.parent = gameObject.transform;
    }

}
