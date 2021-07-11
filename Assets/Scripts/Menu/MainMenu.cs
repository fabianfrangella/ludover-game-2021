using Menu;
using Persistence;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public PlayerStatsDAO dao;
    public InventoryDAO inventoryDao;
    public void StartGame()
    {
        SceneLoader.instance.LoadScene("LoadingScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        dao.LoadPlayerData();
        inventoryDao.LoadItems();
        PlayerStats.instance.LoadStats(dao.playerStatsData);
        PlayerStats.instance.LoadItems(inventoryDao.items);
        SceneLoader.instance.LoadScene(dao.playerStatsData.scene);
    }
    
}
