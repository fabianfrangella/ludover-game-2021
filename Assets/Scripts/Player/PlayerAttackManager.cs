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
        var horizontal = Input.GetAxisRaw("Horizontal");
        isFacingRight = isFacingRight ? horizontal > 0 || horizontal == 0 : horizontal > 0;
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
        bool isAttacking = Input.GetMouseButtonDown(0);
        if (isAttacking && timeSinceLastAttack > 0.2 && playerStaminaManager.stamina >= 40)
        {
            RaycastHit2D hit = GetRayCast();
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyHealthManager>().OnDamageReceived(damage);
                }
            }
            playerAnimationsManager.PlayAttackAnimation();
            playerStaminaManager.OnStaminaLost(40);
            timeSinceLastAttack = 0;

        }
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
