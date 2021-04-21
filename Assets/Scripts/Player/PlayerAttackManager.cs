using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public int damage;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private bool isFacingRight;
    private PlayerStaminaManager playerStaminaManager;
    private PlayerAnimationsManager playerAnimationsManager;
    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        playerStaminaManager = gameObject.GetComponent<PlayerStaminaManager>();
        playerAnimationsManager = gameObject.GetComponent<PlayerAnimationsManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetIsFacingRight();
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
