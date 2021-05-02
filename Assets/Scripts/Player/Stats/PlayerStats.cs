using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float mana;
    public float stamina;

    public float maxHealth;
    public float maxMana;
    public float maxStamina;

    public float meleeDamage;
    public float spellDamage;

    private PlayerExperienceManager playerExperienceManager;

    private void Start()
    {
        playerExperienceManager = GetComponent<PlayerExperienceManager>();
        playerExperienceManager.OnLevelUp += HandleLevelUp;

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
