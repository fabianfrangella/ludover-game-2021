using System;
using Audio;
using Persistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pause
{
    public class SaveGameManager : MonoBehaviour
    {
        public PlayerStatsDAO statsDao;
        public InventoryDAO inventoryDao;
        public TextMeshProUGUI tmp;
        private AudioManager audioManager;
        
        private void Start()
        {
            tmp.enabled = false;
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void FixedUpdate()
        {
            if (tmp.enabled)
            {
                tmp.enabled = false;
            }
        }

        private void Update()
        {
            if (audioManager == null)
            {
                audioManager = FindObjectOfType<AudioManager>();
            }
        }
        
        public void Save()
        {
            tmp.enabled = true;
            audioManager.Play("Press");
            statsDao.SaveIntoJson();
            inventoryDao.SaveIntoJson();
        }
    }
}