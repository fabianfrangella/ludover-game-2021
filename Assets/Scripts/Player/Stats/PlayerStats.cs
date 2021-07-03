using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance = null;
    
    public float health;
    public float mana;
    public float stamina;

    public float maxHealth;
    public float maxMana;
    public float maxStamina;

    public float meleeDamage;
    public float spellDamage;

    public float currentExperience = 0;
    public float nextLevelExperience = 100;
    public int level = 1;
    public int maxLevel = 100;
    private PlayerExperienceManager playerExperienceManager;
    
    private Dictionary<int, int> manaPerLevel;
    private Dictionary<int, int> healthPerLevel;
    private Dictionary<int, int> staminaPerLevel;
    private Dictionary<int, int> spellDamagePerLevel;
    private Dictionary<int, int> meleeDamagePerLevel;
    
    private void Awake() {
        if (!instance)
        {
            instance = this;
            manaPerLevel = new Dictionary<int, int>() { {1, 40}, {2, 60}, {3, 70}, {4, 80}, {5, 90}, {6, 100} };
            healthPerLevel = new Dictionary<int, int>() { {1, 40}, {2, 60}, {3, 70}, {4, 80}, {5, 90}, {6, 100} };
            staminaPerLevel = new Dictionary<int, int>() { {1, 40}, {2, 60}, {3, 70}, {4, 80}, {5, 90}, {6, 100} };
            spellDamagePerLevel = new Dictionary<int, int>() { {1, 5}, {2, 7}, {3, 10}, {4, 12}, {5, 14}, {6, 20} };
            meleeDamagePerLevel = new Dictionary<int, int>() { {1, 10}, {2, 12}, {3, 15}, {4, 18}, {5, 20}, {6, 30} };
        }
        else {
            Destroy(gameObject) ;
            return;
        }
        DontDestroyOnLoad(gameObject) ;
    }

    public void SetExperienceManager(PlayerExperienceManager manager)
    {
        playerExperienceManager = manager;
        manager.OnLevelUp += HandleLevelUp;
    }
    private void Start()
    {
        health = maxHealth;
        mana = maxMana;
        stamina = maxStamina;
    }
    private void HandleLevelUp()
    {
        if (level <= 6)
        {
            maxHealth += healthPerLevel[level];
            health = maxHealth;
            
            maxMana += manaPerLevel[level];
            mana = maxMana;
            
            maxStamina += staminaPerLevel[level];
            stamina = maxStamina;
            
            spellDamage += spellDamagePerLevel[level];
            
            meleeDamage += meleeDamagePerLevel[level];
            return;
        }
        
        maxHealth += healthPerLevel[6];
        health = maxHealth;

        maxStamina += staminaPerLevel[6];
        stamina = maxStamina;

        maxMana += manaPerLevel[6];
        mana = maxMana;

        meleeDamage += meleeDamagePerLevel[6];
        spellDamage += spellDamagePerLevel[6];
    }

  

}
