using Menu;
using Persistence;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public PlayerStatsDAO dao;
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
        PlayerStats.instance.LoadStats(dao.playerStatsData);
        SceneLoader.instance.LoadScene(dao.playerStatsData.scene);
    }
    
}
