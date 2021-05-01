using UnityEngine;

public class Shock : MonoBehaviour
{
    public Vector2 direction;

    public Animator animator;

    public float damage;
    public float speed;
    public int manaCost;

    private bool hasHit;
    private Vector2 prevLoc;
    private Vector2 startPosition;
    private PlayerExperienceManager playerExperienceManager;
    private void Start()
    {
        hasHit = false;
        playerExperienceManager = FindObjectOfType<PlayerExperienceManager>();
        prevLoc = transform.position;
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (direction != null)
        {
            var wayPoint = Vector2.MoveTowards(transform.position, direction, speed);
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, step);
            SetAnimationDirection();
        }
        HandleHits();
        HandleDestroy();
    }

    void HandleDestroy()
    {
        if (!hasHit && Vector2.Distance(transform.position, startPosition) > 5
            || transform.position.Equals(direction))
        {
           Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void SetExtraDamage(float extraDamage)
    {
        damage += extraDamage;
    }

    private void SetAnimationDirection()
    {
        var dir = (Vector2) transform.position - prevLoc;
        prevLoc = transform.position;
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
    }

    private void HandleHits()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.25f);
        foreach (var collider in collisions)
        {
            hasHit = collider.CompareTag(TagEnum.Enemy.ToString());
            if (hasHit)
            {
                var experience = collider.gameObject.GetComponent<EnemyHealthManager>().OnDamageReceived(damage);
                playerExperienceManager.GainExperience(experience);
                Destroy(gameObject);
                break;
            }
        }
        hasHit = false;
    }
}
