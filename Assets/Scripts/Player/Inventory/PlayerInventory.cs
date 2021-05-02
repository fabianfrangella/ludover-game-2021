using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemInterface> items = new List<ItemInterface>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UseHealthPotion();
    }


    private void UseHealthPotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var healthPotion = FindHealthPotion();
            if (healthPotion != null)
            {
                healthPotion.Use();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPotion"))
        {
            PickUp(collision.GetComponent<Item<PlayerHealthManager>>());
        }
    }

    Item<PlayerHealthManager> FindHealthPotion()
    {
        return (Item<PlayerHealthManager>) items.Find(item => item.GetType().Equals(ItemType.HEALTHPOTION));
    }

    Item<T> GetItem<T>(Item<T> item)
    {
        return (Item<T>) items.Find(i => i.Equals(item));
    }

    void DropItem<T>(Item<T> item)
    {
        var it = GetItem(item);
        // Dropea el item frente al personaje
        it.gameObject.transform.position = new Vector2(transform.position.x + 1, transform.position.y - 1);
        it.gameObject.SetActive(true);
    }

    void PickUp<T>(Item<T> item)
    {
        if (item.itemType.Equals(ItemType.HEALTHPOTION))
        {
            item.gameObject.SetActive(false);
            item.SetOwner(gameObject.GetComponent<T>());
            items.Add(item);
        }
    }

}
