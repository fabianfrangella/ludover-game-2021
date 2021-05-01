﻿using UnityEngine;

[SerializeField]
public class Shock : MonoBehaviour
{
    public Vector2 direction;

    public Animator animator;

    public int damage;
    public float speed;

    private bool hasHit;
    private Vector2 prevLoc;
    private Vector2 startPosition;
    private PlayerExperienceManager playerExperienceManager;

    private void Awake()
    {
        hasHit = false;
        direction = Vector2.zero; 
    }
    private void Start()
    {
        playerExperienceManager = FindObjectOfType<PlayerExperienceManager>();
        prevLoc = transform.position;
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (direction != null && !direction.Equals(Vector2.zero))
        {
            transform.Translate(Vector2.MoveTowards(transform.position, direction, speed).normalized * speed * Time.deltaTime);
            SetAnimationDirection();
        }
        HandleHits();
        HandleDestroy();
    }

    void HandleDestroy()
    {
        if (!hasHit && Vector2.Distance(transform.position, startPosition) > 5)
        {
           Destroy(gameObject);
        }
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
                break;
            }
        }
        hasHit = false;
    }
}
