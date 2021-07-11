using System;
using UnityEngine;

namespace Persistence
{
    [Serializable]
    public class ItemData
    {
        [SerializeField]
        private string itemName;

        public ItemData(string name)
        {
            itemName = name;
        }

        public Item ToModel()
        {
            if (itemName.Equals("ManaPotion"))
            {
                return new ManaPotion();
            }

            if (itemName.Equals("HealthPotion"))
            {
                return new HealthPotion();
            }

            return null;
        }
    }
}