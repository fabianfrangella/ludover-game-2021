using System;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class NecromancerAttackManager : MonoBehaviour
    {
        public int invokes = 2;
        private const int InvokesDone = 0;
        private InvokeSkeletons invokeSkeletons;
        private void Start()
        {
            invokeSkeletons = GetComponent<InvokeSkeletons>();
        }

        private void Update()
        {
            if (!AreChildrenAlive() && InvokesDone != invokes)
            {
                invokeSkeletons.Invoke();
            }
        }

        private bool AreChildrenAlive()
        {
            return transform.childCount > 0;
        }
    }
}