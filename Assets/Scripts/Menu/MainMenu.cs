using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScreen");
        SceneLoader.instance.prevScene = "MainMenu";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
