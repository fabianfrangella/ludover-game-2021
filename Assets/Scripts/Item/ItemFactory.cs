using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public List<GameObject> prefabs;
    public static Item CreateItem(ItemEnum item)
    {
        switch (item)
        {
            case ItemEnum.HealthPotion:
                return new HealthPotion();
            case ItemEnum.ManaPotion:
                return new ManaPotion();
            case ItemEnum.StaminaPotion:
                return new StaminaPotion();
            default:
                return null;
        }
    }

    public GameObject GetItemPrefab(ItemEnum item)
    {
        if (item.Equals(ItemEnum.HealthPotion))
        {
            return prefabs.Find(i => i.GetComponent<ItemManager>().item.Equals(ItemEnum.HealthPotion));
        }
        if (item.Equals(ItemEnum.ManaPotion))
        {
            return prefabs.Find(i => i.GetComponent<ItemManager>().item.Equals(ItemEnum.ManaPotion));
        }
        return null;
    }
    
}
