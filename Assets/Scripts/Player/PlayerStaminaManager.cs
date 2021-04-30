using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaManager : MonoBehaviour
{
    public float stamina;
    public float maxStamina;

    public StaminaBar staminaBar;
    private PlayerExperienceManager playerExperienceManager;
    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        staminaBar.SetStamina(stamina);
        staminaBar.SetMaxStamina(maxStamina);
        playerExperienceManager = GetComponent<PlayerExperienceManager>();
        playerExperienceManager.OnLevelUp += HandleLevelUp;
    }

    private void HandleLevelUp()
    {
        maxStamina += maxStamina / 2;
    }

    private void FixedUpdate()
    {
        OnStaminaReceived(0.25f);
        SetStaminaBar();
    }

    public void SetStaminaBar()
    {
        staminaBar.SetStamina(stamina);
        staminaBar.SetMaxStamina(maxStamina);
    }

    public void OnStaminaLost(float stamina)
    {
        if (this.stamina - stamina < 0)
        {
            this.stamina = 0;
            return;
        }
        this.stamina -= stamina;
    }

    public void OnStaminaReceived(float stamina)
    {
        if (this.stamina + stamina >= maxStamina)
        {
            this.stamina = maxStamina;
            return;
        }
        this.stamina += stamina;
    }


}
