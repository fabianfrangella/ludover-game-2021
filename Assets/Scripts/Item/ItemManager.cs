using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemEnum item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagEnum.Player.ToString()))
        {
            PickupItem(collision);
        }
    }

    private void PickupItem(Collider2D collision)
    {
        try
        {
            collision.GetComponent<PlayerInventory>().AddItem(CreateItem());
            Destroy(gameObject);
        }
        catch (InventoryFullException)
        {
            Debug.Log("The inventory is full");
        }
    }

    private Item CreateItem()
    {
        if (item.Equals(ItemEnum.HealthPotion))
        {
            return new HealthPotion();
        }
        if(item.Equals(ItemEnum.ManaPotion))
        {
            return new ManaPotion();
        }
        if(item.Equals(ItemEnum.StaminaPotion))
        {
            return new StaminaPotion();
        }

        return null;
    }
}
