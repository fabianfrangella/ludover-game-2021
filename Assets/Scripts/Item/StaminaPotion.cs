using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : Item<PlayerStaminaManager>
{
    public int staminaAmount = 10;

    private void Start()
    {
        itemType = ItemType.STAMINAPOTION;
    }
    override public void Use()
    {
        owner.OnStaminaReceived(staminaAmount);
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return "{ staminaAmount: 10, itemType: STAMINAPOTION }";
    }
}
