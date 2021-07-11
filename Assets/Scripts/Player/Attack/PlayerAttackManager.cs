using System;
using Audio;
using Hud;
using Pause;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{

    private PlayerAttackState playerAttackState;
    private AudioManager audioManager;
    
    private void Start()
    {
        playerAttackState = GetComponent<PlayerMeleeAttackManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    private void Update()
    {
        if (PauseManager.IsGamePaused()) return;
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
            audioManager.Play("ChangeWeapon");
        }

        var attackMode = FindObjectOfType<AttackMode>();
        if (attackMode != null) attackMode.HighLightAttackMode();
    }

}
