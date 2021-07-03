using UnityEngine;

public class PlayerStaminaManager : MonoBehaviour
{
    public StaminaBar staminaBar;
    

    void Start()
    {
        if (staminaBar != null)
        {
            staminaBar.SetMaxStamina(PlayerStats.instance.maxStamina);
        }
    }

    private void FixedUpdate()
    {
        OnStaminaReceived(0.25f);
        if (staminaBar != null)
        {
            SetStaminaBar();
        }
    }

    public void SetStaminaBar()
    {
        staminaBar.SetStamina(PlayerStats.instance.stamina);
        staminaBar.SetMaxStamina(PlayerStats.instance.maxStamina);
    }

    public void OnStaminaLost(float stamina)
    {
        if (PlayerStats.instance.stamina - stamina < 0)
        {
            PlayerStats.instance.stamina = 0;
            return;
        }
        PlayerStats.instance.stamina -= stamina;
    }

    public void OnStaminaReceived(float stamina)
    {
        if (PlayerStats.instance.stamina + stamina >= PlayerStats.instance.maxStamina)
        {
            PlayerStats.instance.stamina = PlayerStats.instance.maxStamina;
            return;
        }
        PlayerStats.instance.stamina += stamina;
    }


}
