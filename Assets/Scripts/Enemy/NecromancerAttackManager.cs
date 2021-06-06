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
        private bool hasFoundPlayer = false;
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
        }

        private void Update()
        {
            if (invokesDone > invokes && !hasFoundPlayer)
            {
                AttackPlayer();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            hasFoundPlayer = (other.collider.CompareTag(TagEnum.Player.ToString()));
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

        private void AttackPlayer()
        {
            var wayPoint = Vector2.MoveTowards(transform.position, target.transform.position, speed);
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, step);
            SetAnimationDirection();
        }
        
        private void SetAnimationDirection()
        {
            var dir = (Vector2) transform.position - prevLoc;
            prevLoc = transform.position;
            animationManager.SetMovingAnimation(dir.x, dir.y);
        }
        
        private void InvokeShield()
        {
            if (enemyHealthManager.IsAlive())
            {
                enemyHealthManager.SetAbsorption(10000);
                currentShield = Instantiate(shield, transform.position, Quaternion.identity);
                currentShield.transform.parent = transform;
                animationManager.PlayAttackAnimation();
            }
        }

        private void Trigger()
        {
            invokeSkeletons.SetTrigger();
            target = GameObject.FindGameObjectWithTag("Player");
        }
        
    }
}