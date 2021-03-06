using Audio;
using Hud;
using UnityEngine;

public class PlayerMeleeAttackManager : MonoBehaviour, PlayerAttackState
{
    public float attackRate = 5f;
    public float attackDistance = 1f;

    private float nextAttackTime = 0f;

    private PlayerStaminaManager playerStaminaManager;
    private PlayerExperienceManager playerExperienceManager;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerMovementManager playerMovementManager;
    private PlayerHealthManager playerHealthManager;
    private Vector2 directionToAttack;
    private AudioManager audioManager;

    private void Start()
    {
        playerStaminaManager = GetComponent<PlayerStaminaManager>();
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerExperienceManager = GetComponent<PlayerExperienceManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        SetDirectionToAttack();
    }
    private void FixedUpdate()
    {
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
        FindObjectOfType<CursorManager>().SetMagicCursor();
        FindObjectOfType<AttackMode>().isUsingMagic = true;
        return GetComponent<PlayerSpellManager>();
    }

    private void SetDirectionToAttack()
    {
        var direction = playerMovementManager.DirectionWhereIsLooking(); //new Vector2(playerAnimationsManager.lastHorizontal, playerAnimationsManager.lastVertical);
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
        return Time.time >= nextAttackTime && PlayerStats.instance.stamina >= 40 && playerHealthManager.IsAlive();
    }

    private void DoBasicAttack()
    {
        RaycastHit2D[] hits = GetHits();
        if (IsHit(hits))
        {
            HandleHits(hits);
        }
        audioManager.Play("SwordSwing");
        playerAnimationsManager.PlayAttackAnimation();
        playerStaminaManager.OnStaminaLost(40);
    }

    private void HandleHits(RaycastHit2D[] hits)
    {
        foreach (var hit in hits)
        {
            if (!hit.collider.CompareTag(TagEnum.Enemy.ToString())) continue;
            var experience = hit.collider.gameObject.GetComponent<EnemyHealthManager>().OnDamageReceived(PlayerStats.instance.meleeDamage);
            playerExperienceManager.GainExperience(experience);
            break;
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

}


