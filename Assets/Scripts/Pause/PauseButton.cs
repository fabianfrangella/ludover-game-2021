using System;
using Audio;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pause
{
    [Serializable]
    public class PauseButton : MonoBehaviour
    { 
        public Button button;
        protected AudioManager audioManager;
        
        private void Update()
        {
            if (audioManager == null)
            {
                audioManager = FindObjectOfType<AudioManager>();
            }
        }
        
        private void Start()
        {
            button.enabled = false;
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
        
        public void EnableButton()
        {
            button.enabled = true;
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        
        public void DisableButton()
        {
            button.enabled = false;
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}