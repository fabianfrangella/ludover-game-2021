using System;
using System.Collections.Generic;
using Audio;
using Pause;
using Persistence;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maxSize;
    
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (PauseManager.IsGamePaused()) return;
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
        return PlayerStats.instance.items.FindAll(item => item.name.Equals(i)).Count;
    }

    private Item FindItemByName(ItemEnum i)
    {
        return PlayerStats.instance.items.Find(item => item.GetName().Equals(i));
    }

    public void AddItem(Item item)
    {
        if (PlayerStats.instance.items.Count >= maxSize)
        {
            throw new InventoryFullException();
        }
        PlayerStats.instance.items.Add(item);
    }

    private void UseItem(Item item)
    {
        if (item != null)
        {
            audioManager.Play("Potion");
            item.Use(transform);
            PlayerStats.instance.items.Remove(item);
        }
    }
    
}

public class InventoryFullException : Exception
{

}
