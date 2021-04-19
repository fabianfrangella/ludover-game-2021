using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Item<PlayerManaManager>
{
    public int manaAmount = 10;

    private void Start()
    {
        itemType = ItemType.MANAPOTION;
    }
    override public void Use()
    {
        // TODO IMPLEMENTAR
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return "{ manaAmount: 10, itemType: MANAPOTION }";
    }
}
