using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class RestartGameButtonUI : MonoBehaviour
    {
        public Button button;
        private PlayerHealthManager playerHealthManager;
        private void Start()
        {
            button.enabled = false;
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            playerHealthManager = FindObjectOfType<PlayerHealthManager>();
            playerHealthManager.OnPlayerDeath += EnableButton;
        }

        private void EnableButton()
        {
            Debug.Log("Can restart now");
            button.enabled = true;
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        }

        public void RestartGame()
        {
            PlayerStats.instance.health = PlayerStats.instance.maxHealth;
            SceneLoader.instance.LoadScene(SceneLoader.instance.currentScene);
        }
    }
}