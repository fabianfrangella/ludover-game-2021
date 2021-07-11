using System;
using TMPro;
using UnityEngine;

namespace Pause
{
    public class PauseManager : MonoBehaviour
    {
        private static bool _gameIsPaused = false;
        public PauseManager instance;
        public TextMeshProUGUI tmp;
        private void Awake()
        {
            tmp.enabled = false;
            if (!instance)
            {
                instance = this;
            } 
            else 
            {
                Destroy(gameObject) ;
                return;
            }
            DontDestroyOnLoad(gameObject) ;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameIsPaused = !_gameIsPaused;
                PauseGame();
            }
        }

        private void PauseGame ()
        {
            if(_gameIsPaused)
            {
                Time.timeScale = 0f;
                tmp.enabled = true;
                AudioListener.pause = true;
            }
            else 
            {
                Time.timeScale = 1;
                tmp.enabled = false;
                AudioListener.pause = false;
            }
        }

        public static bool IsGamePaused()
        {
            return _gameIsPaused;
        }
    }
}