using Menu;
using UnityEngine;
using UnityEngine.Serialization;

namespace Persistence
{
    public class PlayerStatsDAO : MonoBehaviour
    {
        [SerializeField] public PlayerStatsData playerStatsData = new PlayerStatsData();
        
        public void SaveIntoJson()
        {
            playerStatsData.health = PlayerStats.instance.health;
            playerStatsData.mana = PlayerStats.instance.mana;
            playerStatsData.stamina = PlayerStats.instance.stamina;
            playerStatsData.maxHealth = PlayerStats.instance.maxHealth;
            playerStatsData.maxMana = PlayerStats.instance.maxMana;
            playerStatsData.maxStamina = PlayerStats.instance.maxStamina;
            playerStatsData.level = PlayerStats.instance.level;
            playerStatsData.maxLevel = PlayerStats.instance.maxLevel;
            playerStatsData.meleeDamage = PlayerStats.instance.meleeDamage;
            playerStatsData.spellDamage = PlayerStats.instance.spellDamage;
            playerStatsData.currentExperience = PlayerStats.instance.currentExperience;
            playerStatsData.nextLevelExperience = PlayerStats.instance.nextLevelExperience;
            playerStatsData.scene = SceneLoader.instance.currentScene;
            var stats = JsonUtility.ToJson(playerStatsData);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/_PlayerStatsData.json", stats);
        }

        public void LoadPlayerData()
        {
            playerStatsData = JsonUtility.FromJson<PlayerStatsData> (System.IO.File.ReadAllText(Application.persistentDataPath + "/_PlayerStatsData.json"));
        }
        
    }
}