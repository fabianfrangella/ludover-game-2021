using Menu;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.instance.LoadScene("LoadingScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
