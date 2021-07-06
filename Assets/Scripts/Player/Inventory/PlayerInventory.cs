using System;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maxSize;
    
    private List<Item> items;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        items = new List<Item>() { new HealthPotion(), new HealthPotion() };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && PlayerStats.instance.health < PlayerStats.instance.maxHealth)
        {
            var potion = FindItemByName(ItemEnum.HealthPotion);
            UseItem(potion);
        }
        if (Input.GetKeyDown(KeyCode.E) && PlayerStats.instance.mana < PlayerStats.instance.maxMana)
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
            audioManager.Play("Potion");
            item.Use(transform);
            items.Remove(item);
        }
    }
}

public class InventoryFullException : Exception
{

}
