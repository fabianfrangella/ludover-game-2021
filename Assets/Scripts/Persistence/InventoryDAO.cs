using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Persistence
{
    public class InventoryDAO : MonoBehaviour
    {
        [SerializeField] public List<ItemData> items = new List<ItemData>();

        public void SaveIntoJson()
        {
            items = PlayerStats.instance.items.Select(i => new ItemData(i.name.ToString())).ToList();
            var data = JsonUtility.ToJson(new ItemArray(items));
            System.IO.File.WriteAllText(Application.persistentDataPath + "/_PlayerItemsData.json", data);
        }

        public void LoadItems()
        {
            items = JsonUtility.FromJson<ItemArray>(System.IO.File.ReadAllText(Application.persistentDataPath + "/_PlayerItemsData.json")).items;
        }
        
    }

    [Serializable]
    public class ItemArray
    {
        public List<ItemData> items;

        public ItemArray(List<ItemData> items)
        {
            this.items = items;
        }
    }
}