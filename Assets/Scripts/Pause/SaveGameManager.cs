using Persistence;
using UnityEngine;

namespace Pause
{
    public class SaveGameManager : MonoBehaviour
    {
        public PlayerStatsDAO statsDao;
        public InventoryDAO inventoryDao;

        public void Save()
        {
            statsDao.SaveIntoJson();
            inventoryDao.SaveIntoJson();
        }
    }
}