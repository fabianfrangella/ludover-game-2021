using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerHealthManager playerHealthManager;
    public PlayerStaminaManager playerStaminaManager;
    public PlayerManaManager playerManaManager;

    public event System.Action OnPlayerDeath;
    public event System.Action OnAttacking;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (!playerHealthManager.IsAlive())
        {
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
        }
        playerStaminaManager.OnStaminaReceived(1); 
    }
    // Update is called once per frame
    void Update()
    {
        HandleAttack();
    }

    public bool CanAttack()
    {
        return playerStaminaManager.stamina >= 40;
    }

    void HandleAttack() 
    {
        var isAttacking = Input.GetMouseButtonDown(0);
        if (isAttacking)
        {
            if (OnAttacking != null && playerStaminaManager.stamina >= 40)
            {
                OnAttacking();
                playerStaminaManager.OnStaminaLost(40);
            }
        }
    }
 
}
