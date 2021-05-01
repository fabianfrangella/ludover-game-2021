using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaManager : MonoBehaviour
{

    float mana;
    public float maxMana;

    public ManaBar manaBar;

    private PlayerExperienceManager playerExperienceManager;
    // Start is called before the first frame update

    private void Start()
    {
        playerExperienceManager = GetComponent<PlayerExperienceManager>();
        playerExperienceManager.OnLevelUp += HandleLevelUp;
        mana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    private void Update()
    {
        OnManaReceived(0.10f);
        SetManaBar();
    }

    public void SetManaBar()
    {
        manaBar.SetMana(mana);
        manaBar.SetMaxMana(maxMana);
    }

    void HandleLevelUp()
    {
        maxMana += maxMana / 2;
        mana = maxMana;
    }

    public void OnManaReceived(float mana)
    {
        if (this.mana + mana >= maxMana)
        {
            this.mana = maxMana;
            return;
        }
        this.mana += mana;
    }

    public void OnManaLost(float value)
    {
        if (mana - value <= 0)
        {
            mana = 0;
            return;
        }
        mana -= value;
    }

    public float GetCurrentMana()
    {
        return mana;
    }
}
