using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            button.enabled = true;
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("SafeZone");
        }
    }
}