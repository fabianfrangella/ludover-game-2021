using System.Collections.Generic;
using Persistence;
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

    public Position initialPosition;
    
    private PlayerExperienceManager playerExperienceManager;
    
    private Dictionary<int, int> manaPerLevel;
    private Dictionary<int, int> healthPerLevel;
    private Dictionary<int, int> staminaPerLevel;
    private Dictionary<int, int> spellDamagePerLevel;
    private Dictionary<int, int> meleeDamagePerLevel;

    [SerializeField]
    public List<Item> items;
    
    private void Awake() {
        if (!instance)
        {
            initialPosition = null;
            instance = this;
            manaPerLevel = new Dictionary<int, int>() { {1, 40}, {2, 60}, {3, 70}, {4, 80}, {5, 90}, {6, 100} };
            healthPerLevel = new Dictionary<int, int>() { {1, 40}, {2, 60}, {3, 70}, {4, 80}, {5, 90}, {6, 100} };
            staminaPerLevel = new Dictionary<int, int>() { {1, 40}, {2, 60}, {3, 70}, {4, 80}, {5, 90}, {6, 100} };
            spellDamagePerLevel = new Dictionary<int, int>() { {1, 5}, {2, 7}, {3, 10}, {4, 12}, {5, 14}, {6, 20} };
            meleeDamagePerLevel = new Dictionary<int, int>() { {1, 10}, {2, 12}, {3, 15}, {4, 18}, {5, 20}, {6, 30} };
            items = new List<Item>() { new HealthPotion(), new ManaPotion(), new HealthPotion(), new ManaPotion() };
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

    public void LoadStats(PlayerStatsData data)
    {
        health = data.health;
        mana = data.mana;
        stamina = data.stamina;
        maxHealth = data.maxHealth;
        maxMana = data.maxMana;
        maxStamina = data.maxStamina;
        level = data.level;
        maxLevel = data.maxLevel;
        meleeDamage = data.meleeDamage;
        spellDamage = data.spellDamage;
        currentExperience = data.currentExperience;
        nextLevelExperience = data.nextLevelExperience;
        initialPosition = data.position;
    }
    
    public void LoadItems(List<ItemData> itemsData)
    {
        if (itemsData == null) return;
        items = new List<Item>();
        foreach (var item in itemsData) 
        {
            items.Add(item.ToModel());
        }
    }

}
