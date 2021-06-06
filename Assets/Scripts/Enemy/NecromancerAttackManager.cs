using System;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class NecromancerAttackManager : MonoBehaviour
    {
        public int invokes = 4;
        private int invokesDone = 0;
        private InvokeSkeletons invokeSkeletons;
        private NecromancerAnimationManager animationManager;
        private EnemyHealthManager enemyHealthManager;
        public Transform shield;

        private Transform currentShield;
        private void Start()
        {
            invokeSkeletons = GetComponent<InvokeSkeletons>();
            invokeSkeletons.OnWaveStart += InvokeShield;
            invokeSkeletons.OnWaveFinished += InvokeWave;
            animationManager = GetComponent<NecromancerAnimationManager>();
            enemyHealthManager = GetComponent<EnemyHealthManager>();
            enemyHealthManager.SetAbsorption(10000);
            enemyHealthManager.OnHit += Trigger;
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
                invokesDone++;
            }
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
        }
        
    }
}