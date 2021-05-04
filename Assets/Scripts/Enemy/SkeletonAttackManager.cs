using UnityEngine;

public class SkeletonAttackManager : MonoBehaviour
{

    public float attackDistance;
    public float damage;
    public float attackRate = 5f;
    private float nextAttackTime = 0f;
    private bool hasFoundPlayer;
    private SkeletonAnimationManager animationManager;
    private EnemyMovementManager enemyMovementManager;
    private EnemyHealthManager enemyHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        animationManager = GetComponent<SkeletonAnimationManager>();
        enemyMovementManager = GetComponent<EnemyMovementManager>();
        enemyHealthManager = GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasFoundPlayer)
        {
            Attack();
        }
        Debug.DrawRay(transform.position, enemyMovementManager.GetDirectionWhereIsLooking().normalized, Color.red);
    }

    void Attack()
    {
        if (CanAttack() && enemyHealthManager.IsAlive())
        {
            DoAttack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasFoundPlayer = collision.collider.CompareTag(TagEnum.Player.ToString());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hasFoundPlayer = false;
    }
    private void DoAttack()
    {
        animationManager.PlayAttackAnimation();
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, enemyMovementManager.GetDirectionWhereIsLooking().normalized, attackDistance);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag(TagEnum.Player.ToString()))
            {
                hit.collider.gameObject.GetComponent<PlayerHealthManager>().OnDamageReceived(damage);
            }
        }
        SetNextAttackTime();
    }

    void SetNextAttackTime()
    {
        nextAttackTime = Time.time + 1f / attackRate;
    }

    private bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }
}
