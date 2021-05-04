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
            collision.GetComponent<PlayerInventory>().AddItem(ItemFactory.CreateItem(item));
            Destroy(gameObject);
        }
        catch (InventoryFullException)
        {
            Debug.Log("The inventory is full");
        }
    }
}
