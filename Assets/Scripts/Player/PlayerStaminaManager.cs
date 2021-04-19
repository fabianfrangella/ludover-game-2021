using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaManager : MonoBehaviour
{
    public int stamina;
    public int maxStamina;
    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        OnStaminaReceived(1);
    }

    public void OnStaminaLost(int stamina)
    {
        if (this.stamina - stamina < 0)
        {
            this.stamina = 0;
            return;
        }
        this.stamina -= stamina;
    }

    public void OnStaminaReceived(int stamina)
    {
        if (this.stamina + stamina >= this.maxStamina)
        {
            this.stamina = this.maxStamina;
            return;
        }
        this.stamina += stamina;
    }


}
