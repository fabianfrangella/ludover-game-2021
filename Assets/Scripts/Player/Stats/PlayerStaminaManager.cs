using UnityEngine;

public class PlayerStaminaManager : MonoBehaviour
{
    public StaminaBar staminaBar;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        staminaBar.SetMaxStamina(playerStats.maxStamina);
    }

    private void FixedUpdate()
    {
        OnStaminaReceived(0.25f);
        SetStaminaBar();
    }

    public void SetStaminaBar()
    {
        staminaBar.SetStamina(playerStats.stamina);
        staminaBar.SetMaxStamina(playerStats.maxStamina);
    }

    public void OnStaminaLost(float stamina)
    {
        if (playerStats.stamina - stamina < 0)
        {
            playerStats.stamina = 0;
            return;
        }
        playerStats.stamina -= stamina;
    }

    public void OnStaminaReceived(float stamina)
    {
        if (playerStats.stamina + stamina >= playerStats.maxStamina)
        {
            playerStats.stamina = playerStats.maxStamina;
            return;
        }
        playerStats.stamina += stamina;
    }


}
