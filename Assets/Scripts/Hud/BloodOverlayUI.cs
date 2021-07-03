using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class BloodOverlayUI : MonoBehaviour
    {
        public Image image;
        
        private PlayerHealthManager playerHealthManager;
        private bool hit;
        private void Start()
        {
            playerHealthManager = FindObjectOfType<PlayerHealthManager>();
            playerHealthManager.OnHitReceived += DisplayOverlay;
            image.enabled = false;
        }

        private void Update()
        {
            if (hit) return;
            
            var alpha = .5f - PlayerStats.instance.health / PlayerStats.instance.maxHealth;
            image.enabled = PlayerStats.instance.health < PlayerStats.instance.maxHealth;
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