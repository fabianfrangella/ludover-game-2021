using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hud
{
    public class RestartOnFinishButton : MonoBehaviour
    {
        public Button button;

        private EndGameManager endGameManager;
        private void Start()
        {
            button.enabled = false;
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            endGameManager = FindObjectOfType<EndGameManager>();
            if (endGameManager != null)
            {
                endGameManager.OnGameEnd += EnableButton;
            }
        }
        
        private void EnableButton()
        {
            Debug.Log("Can restart now");
            button.enabled = true;
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        
        public void Restart()
        {
            SceneManager.LoadScene("SafeZone");
        }
    }
}