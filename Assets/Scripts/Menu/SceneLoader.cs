using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance = null;
        
        public string prevScene = "MainMenu";

        public void LoadScene(string currentScene, string nextScene)
        {
            instance.prevScene = currentScene;
            SceneManager.LoadScene(nextScene);
        }
        
        private void Awake() {
            if(!instance)
                instance = this;
            else {
                Destroy(gameObject) ;
                return;
            }
            DontDestroyOnLoad(gameObject) ;
        }
    }
}