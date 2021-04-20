using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{

    public int damage;
    float timeSinceLastAttack;
    private bool isFacingRight;
    private PlayerStaminaManager playerStaminaManager;
    private PlayerAnimationsManager playerAnimationsManager;
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        playerStaminaManager = gameObject.GetComponent<PlayerStaminaManager>();
        playerAnimationsManager = gameObject.GetComponent<PlayerAnimationsManager>();
        timeSinceLastAttack = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetIsFacingRight();
        SetTimeSinceLastAttack();
        // para ver el raycast
        /*
        if (isFacingRight)
        {
            Debug.DrawRay(new Vector2(transform.position.x + 0.55F, transform.position.y - 0.8F), isFacingRight ? Vector2.right : Vector2.left);
        } else
        {
            Debug.DrawRay(new Vector2(transform.position.x - 0.55F, transform.position.y - 0.8F), isFacingRight ? Vector2.right : Vector2.left);
        }
        */
    }

    private void Update()
    {
        HandleAttack();
        HandleStopAttack();
    }

    private void SetTimeSinceLastAttack()
    {
        timeSinceLastAttack = timeSinceLastAttack + Time.deltaTime;
    }

    void HandleStopAttack()
    {
        if (timeSinceLastAttack > 0.2)
        {
            playerAnimationsManager.StopAttackAnimation();
        }
    }

    void HandleAttack()
    {
        if (IsAttacking())
        {
            HandleRaycast();
        }
    }

    private bool IsAttacking()
    {
        bool isAttacking = Input.GetMouseButtonDown(0);
        return isAttacking && timeSinceLastAttack > 0.2 && playerStaminaManager.stamina >= 40;
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
        timeSinceLastAttack = 0;
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

    private void SetIsFacingRight()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        isFacingRight = isFacingRight ? horizontal > 0 || horizontal == 0 : horizontal > 0;
    }
    RaycastHit2D GetRayCast()
    {
        if (isFacingRight)
        {
            return Physics2D.Raycast(new Vector2(transform.position.x + 0.55F, transform.position.y - 0.8F), isFacingRight ? Vector2.right : Vector2.left);
        }
        return Physics2D.Raycast(new Vector2(transform.position.x - 0.55F, transform.position.y - 0.8F), isFacingRight ? Vector2.right : Vector2.left);
    }
}
