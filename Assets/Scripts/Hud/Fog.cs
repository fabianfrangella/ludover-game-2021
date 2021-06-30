using System;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class Fog : MonoBehaviour
    {
        public Image fog;
        private EndGameManager endGameManager;

        private void Start()
        {
            endGameManager = FindObjectOfType<EndGameManager>();
            endGameManager.OnGameEnd += DisableFog;
        }

        private void DisableFog()
        {
            fog.enabled = false;
        }
    }
}