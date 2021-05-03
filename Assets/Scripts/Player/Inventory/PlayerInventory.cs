using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items;

    public int maxSize;

    void Start()
    {
        items = new List<Item>() { new HealthPotion(), new HealthPotion() };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var potion = FindHealthPotion();
            UseItem(potion);
        }
    }

    public void AddItem(Item item)
    {
        if (items.Count >= maxSize)
        {
            throw new InventoryFullException();
        }
        items.Add(item);
    }

    public Dictionary<string, int> GetItemsQuantities()
    {
        var dict = new Dictionary<string, int>();
        foreach (var item in items)
        {
            if (dict.TryGetValue(item.GetName().ToString(), out int val))
            {
                dict[item.GetName().ToString()] = val+1;
                continue;
            }

            dict.Add(item.GetName().ToString(), 1);
        }
        return dict;
    }
    public List<string> GetItemsNames()
    {
        List<string> mappedItems = new List<string>();
        foreach (var item in items)
        {
            mappedItems.Add(item.GetName().ToString());
        }
        return mappedItems;
    }

    private Item FindHealthPotion()
    {
        return items.Find(item => item.GetName().Equals(ItemEnum.HealthPotion));
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
