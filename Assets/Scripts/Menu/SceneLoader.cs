using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance = null;

        public string currentScene = "MainMenu";
        public string prevScene;
        
        public List<string> enabledButtons = new List<string>();
        
        private void Awake() {
            if(!instance)
                instance = this;
            else {
                Destroy(gameObject) ;
                return;
            }
            enabledButtons.Add("SafeZoneButton");
            enabledButtons.Add("OpenLandsButton");
            DontDestroyOnLoad(gameObject) ;
        }

        public void SetButtons(List<string> buttons)
        {
            enabledButtons = buttons;
        }

        public void LoadScene(string scene)
        {
            prevScene = currentScene;
            currentScene = scene;
            SceneManager.LoadScene(scene);
        }
    }
}