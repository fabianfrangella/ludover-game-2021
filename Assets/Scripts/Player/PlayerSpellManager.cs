using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    public Shock shockSpellPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CastShockSpell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void CastShockSpell(Vector2 direction)
    {
        var shock = Instantiate(shockSpellPrefab, transform.position, Quaternion.identity);
        shock.transform.parent = gameObject.transform;
        shock.SetDirection(direction);
    }

}
