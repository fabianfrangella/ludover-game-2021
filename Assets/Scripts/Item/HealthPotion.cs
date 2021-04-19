using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item<PlayerHealthManager>
{
    public int healingAmount = 10;

    private void Start()
    {
        itemType = ItemType.HEALTHPOTION;
    }
    override public void Use()
    {
        owner.OnHealing(healingAmount);
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return "{ healingAmount: 10, itemType: HEALTHPOTION }";
    }
}
