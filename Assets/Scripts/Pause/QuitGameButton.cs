using Enemy;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pause
{
    public class QuitGameButton : MonoBehaviour
    {
        public Button button;
        
        private void Start()
        {
            button.enabled = false;
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
        
        public void EnableButton()
        {
            if (SceneLoader.instance.currentScene.Equals("LoadingScreen")) return;
            button.enabled = true;
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        
        public void DisableButton()
        {
            if (SceneLoader.instance.currentScene.Equals("LoadingScreen")) return;
            button.enabled = false;
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}