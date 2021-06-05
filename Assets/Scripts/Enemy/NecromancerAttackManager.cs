﻿using System;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class NecromancerAttackManager : MonoBehaviour
    {
        public int invokes = 5;
        private int invokesDone = 0;
        private InvokeSkeletons invokeSkeletons;
        private NecromancerAnimationManager animationManager;
        private EnemyHealthManager enemyHealthManager;
        private bool hasBeenTriggered;
        private void Start()
        {
            invokeSkeletons = GetComponent<InvokeSkeletons>();
            animationManager = GetComponent<NecromancerAnimationManager>();
            enemyHealthManager = GetComponent<EnemyHealthManager>();
            enemyHealthManager.OnHit += Trigger;
            enemyHealthManager.SetAbsorption(10000);
        }

        private void Update()
        {
            if (hasBeenTriggered)
            {
                InvokeSkeletons();
            }
            
        }

        private void InvokeSkeletons()
        {
            if (enemyHealthManager.IsAlive() && !AreChildrenAlive() && invokesDone != invokes)
            {
                enemyHealthManager.SetAbsorption(1000);
                animationManager.PlayAttackAnimation();
                invokeSkeletons.Invoke();
                invokesDone++;
            }
            if (!AreChildrenAlive())
            {
                enemyHealthManager.SetAbsorption(0);
            }
        }

        private bool AreChildrenAlive()
        {
            return invokeSkeletons.AreChildrenAlive();
        }

        private void Trigger()
        {
            hasBeenTriggered = true;
        }
    }
}