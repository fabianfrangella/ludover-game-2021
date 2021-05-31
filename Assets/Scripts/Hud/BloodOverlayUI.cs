using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class BloodOverlayUI : MonoBehaviour
    {
        public Image image;

        private PlayerStats playerStats;
        private PlayerHealthManager playerHealthManager;
        private bool hit;
        private void Start()
        {
            playerStats = FindObjectOfType<PlayerStats>();
            playerHealthManager = FindObjectOfType<PlayerHealthManager>();
            playerHealthManager.OnHitReceived += DisplayOverlay;
            image.enabled = false;
        }

        private void Update()
        {
            if (hit) return;
            
            var alpha = .5f - playerStats.health / playerStats.maxHealth;
            image.enabled = playerStats.health < playerStats.maxHealth;
            image.color = new Color(image.color.r, image.color.g, image.color.b,
                alpha > 0.5f ? 0.5f : alpha);
        }


        private void DisplayOverlay()
        {
            StartCoroutine(nameof(DisplayOverlayRoutine));
        }
        private IEnumerator DisplayOverlayRoutine()
        {
            hit = true;
            image.enabled = true;
            image.color = new Color(image.color.r, image.color.g, image.color.b, .1f);
            yield return new WaitForSeconds(0.5f);
            hit = false;
        }
    }
}