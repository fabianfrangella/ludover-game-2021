using UnityEngine;

[SerializeField]
public class Shock : MonoBehaviour
{
    public Vector2 direction;

    public Animator animator;

    public int damage;
    public float speed;

    private Vector2 prevLoc;

    private PlayerExperienceManager playerExperienceManager;

    private void Awake()
    {
        direction = Vector2.zero; 
    }
    private void Start()
    {
        playerExperienceManager = FindObjectOfType<PlayerExperienceManager>();
        prevLoc = transform.position;
    }

    void FixedUpdate()
    {
        if (direction != null && !direction.Equals(Vector2.zero))
        {
            transform.Translate(Vector2.MoveTowards(transform.position, direction, speed).normalized * speed *Time.deltaTime);
            SetAnimationDirection();
        }
        HandleDestroy();
    }

    void HandleDestroy()
    {
        if (Vector2.Distance(direction, transform.position) < 1)
        {
            Destroy(gameObject);
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
