using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropManager : MonoBehaviour
{
    public List<ItemEnum> droppableItems;

    private ItemFactory itemFactory;
    private EnemyHealthManager enemyHealthManager;

    private void Start()
    {
        itemFactory = GetComponent<ItemFactory>();
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        enemyHealthManager.OnDeath += DropItem;
    }

    private void DropItem()
    {
        var random = Random.Range(0, 2);
        if (random < 1)
        {
            Instantiate(itemFactory.GetItemPrefab(ItemEnum.HealthPotion), transform.position, Quaternion.identity);
            return;
        }
        Instantiate(itemFactory.GetItemPrefab(ItemEnum.ManaPotion), transform.position, Quaternion.identity);
    }
}
