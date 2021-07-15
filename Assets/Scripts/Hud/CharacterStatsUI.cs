using System;
using TMPro;
using UnityEngine;

namespace Hud
{
    public class CharacterStatsUI : MonoBehaviour
    {
        public Transform stats;
        
        public Transform health;
        public Transform mana;
        public Transform stamina;
        public Transform level;
        public Transform melee;
        public Transform spell;
        public Transform experience;

        private TextMeshProUGUI healthText;
        private TextMeshProUGUI manaText;
        private TextMeshProUGUI staminaText;
        private TextMeshProUGUI levelText;
        private TextMeshProUGUI experienceText;
        private TextMeshProUGUI meleeDamageText;
        private TextMeshProUGUI spellDamageText;
        
        
        private bool isActive = false;

        private void Start()
        {
            stats.gameObject.SetActive(false);
            healthText = health.GetComponent<TextMeshProUGUI>();
            manaText = mana.GetComponent<TextMeshProUGUI>();
            staminaText = stamina.GetComponent<TextMeshProUGUI>();
            levelText = level.GetComponent<TextMeshProUGUI>();
            experienceText = experience.GetComponent<TextMeshProUGUI>();
            meleeDamageText = melee.GetComponent<TextMeshProUGUI>();
            spellDamageText = spell.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isActive = !isActive;
                stats.gameObject.SetActive(isActive);
            }
            if (isActive)
            {
                healthText.text = "Health: " + PlayerStats.instance.health + "/" + PlayerStats.instance.maxHealth;
                manaText.text = "Mana: " + Math.Round(PlayerStats.instance.mana) + "/" + Math.Round(PlayerStats.instance.maxMana);
                staminaText.text = "Stamina: " + Math.Round(PlayerStats.instance.stamina) + "/" + Math.Round(PlayerStats.instance.maxStamina);
                levelText.text = "Level: " + PlayerStats.instance.level;
                meleeDamageText.text = "Melee Damage: " + PlayerStats.instance.meleeDamage;
                spellDamageText.text = "Spell Damage: " + PlayerStats.instance.spellDamage;
                experienceText.text = "Experience: " + Math.Round(PlayerStats.instance.currentExperience) + "/" + Math.Round(PlayerStats.instance.nextLevelExperience);
            }
        }
    }
}