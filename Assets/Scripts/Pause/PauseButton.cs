using System;
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