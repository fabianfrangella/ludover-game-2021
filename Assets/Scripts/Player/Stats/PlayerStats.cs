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

    private void Awake() {
        if(!instance)
            instance = this;
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
    void HandleLevelUp()
    {
        maxHealth += maxHealth / 2;
        health = maxHealth;

        maxStamina += maxStamina / 2;
        stamina = maxStamina;

        maxMana += maxMana / 2;
        mana = maxMana;

        meleeDamage += meleeDamage += meleeDamage / 2;
        spellDamage += spellDamage += spellDamage / 2;
    }

}
