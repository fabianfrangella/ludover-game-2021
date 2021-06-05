using System;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class NecromancerAttackManager : MonoBehaviour
    {
        public int invokes = 5;
        private int invokesDone = 0;
        private InvokeSkeletons invokeSkeletons;
        private void Start()
        {
            invokeSkeletons = GetComponent<InvokeSkeletons>();
        }

        private void Update()
        {
            if (!AreChildrenAlive() && invokesDone != invokes)
            {
                invokeSkeletons.Invoke();
                invokesDone++;
            }
        }

        private bool AreChildrenAlive()
        {
            return transform.childCount > 0;
        }
    }
}