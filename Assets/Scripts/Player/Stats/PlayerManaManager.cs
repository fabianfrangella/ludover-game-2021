using UnityEngine;

public class PlayerManaManager : MonoBehaviour
{
    public ManaBar manaBar;
    
    // Start is called before the first frame update

    private void Start()
    {
        if (manaBar != null)
        {
            manaBar.SetMaxMana(PlayerStats.instance.maxMana);
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
        manaBar.SetMana(PlayerStats.instance.mana);
        manaBar.SetMaxMana(PlayerStats.instance.maxMana);
    }

    public void OnManaReceived(float mana)
    {
        if (PlayerStats.instance.mana + mana >= PlayerStats.instance.maxMana)
        {
            PlayerStats.instance.mana = PlayerStats.instance.maxMana;
            return;
        }
        PlayerStats.instance.mana += mana;
    }

    public void OnManaLost(float value)
    {
        if (PlayerStats.instance.mana - value <= 0)
        {
            PlayerStats.instance.mana = 0;
            return;
        }
        PlayerStats.instance.mana -= value;
    }

    public float GetCurrentMana()
    {
        return PlayerStats.instance.mana;
    }
}
