using UnityEngine;

public class PlayerManaManager : MonoBehaviour
{
    public ManaBar manaBar;

    private PlayerStats playerStats;
    // Start is called before the first frame update

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        if (manaBar != null)
        {
            manaBar.SetMaxMana(playerStats.maxMana);
        }
    }

    private void FixedUpdate()
    {
        OnManaReceived(0.30f);
        if (manaBar != null)
        {
            SetManaBar();
        }
    }

    public void SetManaBar()
    {
        manaBar.SetMana(playerStats.mana);
        manaBar.SetMaxMana(playerStats.maxMana);
    }

    public void OnManaReceived(float mana)
    {
        if (playerStats.mana + mana >= playerStats.maxMana)
        {
            playerStats.mana = playerStats.maxMana;
            return;
        }
        playerStats.mana += mana;
    }

    public void OnManaLost(float value)
    {
        if (playerStats.mana - value <= 0)
        {
            playerStats.mana = 0;
            return;
        }
        playerStats.mana -= value;
    }

    public float GetCurrentMana()
    {
        return playerStats.mana;
    }
}
