using Enemy;
using TMPro;
using UnityEngine;

namespace Hud
{
    public class EndGameText : MonoBehaviour
    {
        public TextMeshProUGUI tmp;
        private EndGameManager endGameManager;
        private void Start()
        {
            tmp.enabled = false;
            endGameManager = FindObjectOfType<EndGameManager>();
            if (endGameManager != null)
            {
                endGameManager.OnGameEnd += EnableText;
            }
        }

        private void EnableText()
        {
            tmp.enabled = true;
        }
        
    }
}