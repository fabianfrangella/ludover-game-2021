using UnityEngine;

public class Shock : MonoBehaviour
{
    public Vector2 direction;

    public Animator animator;

    public int damage;
    public float speed;
    public float distance;

    private Vector2 prevLoc;
    private PlayerExperienceManager playerExperienceManager;

    private void Start()
    {
        playerExperienceManager = transform.parent.GetComponent<PlayerExperienceManager>();
        prevLoc = transform.position;
    }

    void FixedUpdate()
    {
        if (direction != null)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            SetAnimationDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void SetAnimationDirection()
    {
        var dir = (Vector2) transform.position - prevLoc;
        prevLoc = transform.position;
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
    }

    private void HandleHit(Collider2D collider)
    {
        if (collider.CompareTag(TagEnum.Enemy.ToString()))
        {
            var experience = collider.gameObject.GetComponent<EnemyHealthManager>().OnDamageReceived(damage);
            playerExperienceManager.GainExperience(experience);
        }
    }
}
