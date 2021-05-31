using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maxSize;
    
    private List<Item> items;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        items = new List<Item>() { new HealthPotion(), new HealthPotion() };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerStats.health < playerStats.maxHealth)
        {
            var potion = FindItemByName(ItemEnum.HealthPotion);
            UseItem(potion);
        }
        if (Input.GetKeyDown(KeyCode.E) && playerStats.mana < playerStats.maxMana)
        {
            var potion = FindItemByName(ItemEnum.ManaPotion);
            UseItem(potion);
        }
    }

    public int FindItemQuantity(ItemEnum i)
    {
        return items.FindAll(item => item.name.Equals(i)).Count;
    }

    private Item FindItemByName(ItemEnum i)
    {
        return items.Find(item => item.GetName().Equals(i));
    }

    public void AddItem(Item item)
    {
        if (items.Count >= maxSize)
        {
            throw new InventoryFullException();
        }
        items.Add(item);
    }

    private void UseItem(Item item)
    {
        if (item != null)
        {
            item.Use(transform);
            items.Remove(item);
        }
    }
}

public class InventoryFullException : Exception
{

}
