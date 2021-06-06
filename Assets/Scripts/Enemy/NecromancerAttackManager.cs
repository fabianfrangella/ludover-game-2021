using System;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class NecromancerAttackManager : MonoBehaviour
    {
        public int invokes = 4;
        public float speed = 1.5f;
        public Transform shield;
        
        private int invokesDone = 0;
        private InvokeSkeletons invokeSkeletons;
        private NecromancerAnimationManager animationManager;
        private EnemyHealthManager enemyHealthManager;
        private Transform currentShield;
        private GameObject target;
        private Vector2 prevLoc;
        private Rigidbody2D rb;
        private bool hasFoundPlayer = false;
        private Vector2 direction;
        
        public float attackDistance;
        public float damage;
        public float attackRate = 5f;
        private float nextAttackTime = 0f;
        private void Start()
        {
            invokeSkeletons = GetComponent<InvokeSkeletons>();
            invokeSkeletons.OnWaveStart += InvokeShield;
            invokeSkeletons.OnWaveFinished += InvokeWave;
            animationManager = GetComponent<NecromancerAnimationManager>();
            enemyHealthManager = GetComponent<EnemyHealthManager>();
            enemyHealthManager.SetAbsorption(10000);
            enemyHealthManager.OnHit += Trigger;
            prevLoc = transform.position;
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (hasFoundPlayer)
            {
                Attack();
            }
            if (rb.velocity != Vector2.zero) direction = rb.velocity;
            Debug.DrawRay(transform.position, direction, Color.red);
            if (invokesDone > invokes && !hasFoundPlayer)
            {
                FindPlayer();
            }
        }
        
        private void Attack()
        {
            if (CanAttack() && enemyHealthManager.IsAlive())
            {
                DoAttack();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            hasFoundPlayer = (other.collider.CompareTag(TagEnum.Player.ToString()));
            if (hasFoundPlayer) rb.velocity = Vector2.zero;
            animationManager.SetIdle(hasFoundPlayer);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            hasFoundPlayer = false;
            animationManager.SetIdle(hasFoundPlayer);
        }

        private void InvokeWave()
        {
            if (currentShield != null)
            {
                enemyHealthManager.SetAbsorption(0);
                Destroy(currentShield.gameObject);
            }
            if (invokesDone < invokes)
            {
                invokeSkeletons.Invoke();
            }
            invokesDone++;
        }

        private void FindPlayer()
        {
            var wayPoint = Vector2.MoveTowards(transform.position, target.transform.position, speed);
            rb.velocity = (wayPoint - rb.position).normalized * speed;
            SetAnimationDirection();
        }
        
        private void SetAnimationDirection()
        {
            if (rb.velocity == Vector2.zero) return;
            animationManager.SetMovingAnimation(rb.velocity.x, rb.velocity.y);
        }
        
        private void InvokeShield()
        {
            if (!enemyHealthManager.IsAlive()) return;
            
            enemyHealthManager.SetAbsorption(10000);
            currentShield = Instantiate(shield, transform.position, Quaternion.identity);
            currentShield.transform.parent = transform;
            animationManager.PlayCastAnimation();
            
        }

        private void Trigger()
        {
            invokeSkeletons.SetTrigger();
            target = GameObject.FindGameObjectWithTag("Player");
        }
        
        private void DoAttack()
        {
            animationManager.PlayAttackAnimation();
            animationManager.SetMovingAnimation(direction.x, direction.y);
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
}