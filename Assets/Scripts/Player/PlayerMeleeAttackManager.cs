using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackManager : MonoBehaviour, PlayerAttackState
{

    public int damage;
    public float attackRate = 5f;
    public float attackDistance = 1f;
    float nextAttackTime = 0f;

    private PlayerStaminaManager playerStaminaManager;
    private PlayerExperienceManager playerExperienceManager;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerMovementManager playerMovementManager;

    private Vector2 directionToAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerStaminaManager = GetComponent<PlayerStaminaManager>();
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerExperienceManager = GetComponent<PlayerExperienceManager>();
        playerExperienceManager.OnLevelUp += HandleLevelUp;
    }

    void FixedUpdate()
    {
        SetDirectionToAttack();
        var swordPosition = new Vector2(transform.position.x, transform.position.y - 0.35f);
        Debug.DrawRay(swordPosition, directionToAttack.normalized, Color.red);
    }

    public void HandleAttack()
    {
        if (CanAttack())
        {
            DoBasicAttack();
            SetNextAttackTime();
        }
    }

    public PlayerAttackState GetNextState()
    {
        playerAnimationsManager.SetIsUsingMagic(true);
        return GetComponent<PlayerSpellManager>();
    }

    private void SetDirectionToAttack()
    {
        var direction = playerMovementManager.DirectionWhereIsMoving();
        var standingPosition = new Vector2(0, 0);
        if (!standingPosition.Equals(direction))
        {
            directionToAttack = direction;
        }
    }

    private void SetNextAttackTime()
    {
        nextAttackTime = Time.time + 1f / attackRate;
    }
    private bool CanAttack()
    {
        return Time.time >= nextAttackTime && playerStaminaManager.stamina >= 40;
    }

    private void DoBasicAttack()
    {
        RaycastHit2D[] hits = GetHits();
        if (IsHit(hits))
        {
            HandleHits(hits);
        }
        playerAnimationsManager.PlayAttackAnimation();
        playerStaminaManager.OnStaminaLost(40);
    }

    private void HandleHits(RaycastHit2D[] hits)
    {
        foreach (var hit in hits)
        {
            HandleHit(hit);
        }
    }

    private void HandleHit(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag(TagEnum.Enemy.ToString()))
        {
            var experience = hit.collider.gameObject.GetComponent<EnemyHealthManager>().OnDamageReceived(damage);
            playerExperienceManager.GainExperience(experience);
        }
    }

    private bool IsHit(RaycastHit2D[] hit)
    {
        return hit.Length > 0;
    }

    private RaycastHit2D[] GetHits()
    {
        var swordPosition = new Vector2(transform.position.x, transform.position.y - 0.35f);
        Debug.DrawRay(swordPosition, directionToAttack.normalized, Color.red);

        return Physics2D.RaycastAll(swordPosition, directionToAttack.normalized, attackDistance);
    }

    private void HandleLevelUp()
    {
        damage += damage / 2;
    }

}


