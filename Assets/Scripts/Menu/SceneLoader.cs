using UnityEngine;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance = null;
        
        public string prevScene = "MainMenu";

        private void Awake() {
            if(!instance )
                instance = this;
            else {
                Destroy(gameObject) ;
                return;
            }
            DontDestroyOnLoad(gameObject) ;
        }
    }
}