using UnityEngine;

public class SkeletonAttackManager : MonoBehaviour
{

    public float attackDistance;
    public float damage;
    public float attackRate = 5f;
    private float nextAttackTime = 0f;
    private bool hasFoundPlayer;
    private SkeletonAnimationManager animationManager;
    private Rigidbody2D rb;
    private EnemyHealthManager enemyHealthManager;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        animationManager = GetComponent<SkeletonAnimationManager>();
        rb = GetComponent<Rigidbody2D>();
        enemyHealthManager = GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (hasFoundPlayer)
        {
            Attack();
        }

        if (rb.velocity != Vector2.zero) direction = rb.velocity;
        Debug.DrawRay(transform.position, direction, Color.red);
    }

    private void Attack()
    {
        if (CanAttack() && enemyHealthManager.IsAlive())
        {
            DoAttack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasFoundPlayer = collision.collider.CompareTag(TagEnum.Player.ToString()) 
                         && collision.collider.GetComponent<PlayerHealthManager>().IsAlive();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hasFoundPlayer = false;
    }
    private void DoAttack()
    {
        animationManager.PlayAttackAnimation();
        var hits = Physics2D.RaycastAll(transform.position, direction, attackDistance);
        foreach (var hit in hits)
        {
            AttackPlayer(hit);
        }
        SetNextAttackTime();
    }

    private void AttackPlayer(RaycastHit2D hit)
    {
        if (!hit.collider.CompareTag(TagEnum.Player.ToString())) return;
        var healthManager = hit.collider.gameObject.GetComponent<PlayerHealthManager>();
        healthManager.OnDamageReceived(damage);
        if (!healthManager.IsAlive())
        {
            hasFoundPlayer = false;
        }
    }

    private void SetNextAttackTime()
    {
        nextAttackTime = Time.time + 2f / attackRate;
    }

    private bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }
}
