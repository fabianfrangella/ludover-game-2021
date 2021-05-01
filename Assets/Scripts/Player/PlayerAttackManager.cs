using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public int damage;
    public float attackRate = 5f;
    public float attackDistance = 1f;
    float nextAttackTime = 0f;

    private PlayerStaminaManager playerStaminaManager;
    private PlayerExperienceManager playerExperienceManager;
    private PlayerAnimationsManager playerAnimationsManager;
    private PlayerMovementManager playerMovementManager;
    private PlayerSpellManager playerSpellManager;

    private Vector2 directionToAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerStaminaManager = GetComponent<PlayerStaminaManager>();
        playerAnimationsManager = GetComponent<PlayerAnimationsManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerExperienceManager = GetComponent<PlayerExperienceManager>();
        playerSpellManager = GetComponent<PlayerSpellManager>();
        playerExperienceManager.OnLevelUp += HandleLevelUp;
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
        if (Input.GetMouseButtonDown(1))
        {
            playerSpellManager.CastShockSpell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        HandleBasicAttack();
    }

    void HandleBasicAttack()
    {
        if (IsAttackingAndCanAttack())
        {
            DoBasicAttack();
            SetNextAttackTime();
        }
    }

    private void SetNextAttackTime()
    {
        nextAttackTime = Time.time + 1f / attackRate;
    }
    private bool IsAttackingAndCanAttack()
    {
        bool isAttacking = Input.GetMouseButtonDown(0);
        return isAttacking && Time.time >= nextAttackTime && playerStaminaManager.stamina >= 40;
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
