using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropManager : MonoBehaviour
{
    public List<ItemEnum> droppableItems;
    public int numberOfDrops;

    private int drops = 0;
    private ItemFactory itemFactory;
    private EnemyHealthManager enemyHealthManager;

    private void Start()
    {
        numberOfDrops = 1;
        itemFactory = GetComponent<ItemFactory>();
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        enemyHealthManager.OnDeath += DropItem;
    }

    private void DropItem()
    {
        while (drops < numberOfDrops)
        {
            drops++;
            var random = Random.Range(0, 2);
            if (random < 1)
            {
                Instantiate(itemFactory.GetItemPrefab(ItemEnum.HealthPotion), transform.position, Quaternion.identity);
                continue;
            }
            Instantiate(itemFactory.GetItemPrefab(ItemEnum.ManaPotion), transform.position, Quaternion.identity);
        }
    }
}
