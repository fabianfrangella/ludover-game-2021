using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaManager : MonoBehaviour
{

    float mana;
    public float maxMana;
    // Start is called before the first frame update

    private void Start()
    {
        mana = maxMana;
    }

    private void Update()
    {
        OnManaReceived(0.25f);
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
