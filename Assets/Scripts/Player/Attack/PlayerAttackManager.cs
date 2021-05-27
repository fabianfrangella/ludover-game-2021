using System;
using Hud;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{

    private PlayerAttackState playerAttackState;
    
    private void Start()
    {
        playerAttackState = GetComponent<PlayerMeleeAttackManager>();
    }
    
    private void Update()
    {
        HandleAttack();
        HandleChangeAttackMode();
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAttackState.HandleAttack();
        }
    }

    private void HandleChangeAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAttackState = playerAttackState.GetNextState();
        }

        FindObjectOfType<AttackMode>().HighLightAttackMode();
    }

}
