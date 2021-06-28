using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class LoadingScreen : MonoBehaviour
    {
        public Button button;

        private void Start()
        {
            button.enabled = false;
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }

        private void Update()
        {
            StartCoroutine(nameof(EnableButton));
        }

        private IEnumerator EnableButton()
        {
            yield return new WaitForSeconds(5);
            button.enabled = true;
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        public void EnterDungeon()
        {
            SceneManager.LoadScene("Dungeon");
        }

        public void GoToSafeZone()
        {
            SceneManager.LoadScene("SafeZone");
        }
    }
}