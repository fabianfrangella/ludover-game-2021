using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public int damage;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private PlayerStaminaManager playerStaminaManager;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerMovementManager playerMovementManager;

    private Vector2 directionToAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerStaminaManager = gameObject.GetComponent<PlayerStaminaManager>();
        playerAnimationsManager = gameObject.GetComponent<PlayerAnimationsManager>();
        playerMovementManager = gameObject.GetComponent<PlayerMovementManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetDirectionToAttack();
        var swordPosition = new Vector2(transform.position.x, transform.position.y - 0.35f);
        Debug.DrawRay(swordPosition, directionToAttack.normalized, Color.red);

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

    private void Update()
    {
        HandleAttack();
    }

    void HandleAttack()
    {
        if (IsAttacking())
        {
            HandleRaycast();
            SetNextAttackTime();
        }
    }

    private void SetNextAttackTime()
    {
        nextAttackTime = Time.time + 1f / attackRate;
    }
    private bool IsAttacking()
    {
        bool isAttacking = Input.GetMouseButtonDown(0);
        return isAttacking && Time.time >= nextAttackTime && playerStaminaManager.stamina >= 40;
    }

    private void HandleRaycast()
    {
        RaycastHit2D hit = GetRayCast();
        if (IsHit(hit))
        {
            HandleHit(hit);
        }
        playerAnimationsManager.PlayAttackAnimation();
        playerStaminaManager.OnStaminaLost(40);
    }

    private void HandleHit(RaycastHit2D hit)
    {
        if (hit.collider.tag == "Enemy")
        {
            hit.collider.gameObject.GetComponent<EnemyHealthManager>().OnDamageReceived(damage);
        }
    }

    private bool IsHit(RaycastHit2D hit)
    {
        return hit.collider != null;
    }

    RaycastHit2D GetRayCast()
    {
        var swordPosition = new Vector2(transform.position.x, transform.position.y - 0.35f);
        Debug.DrawRay(swordPosition, directionToAttack.normalized, Color.red);
        return Physics2D.Raycast(swordPosition, directionToAttack.normalized);
    }
}
