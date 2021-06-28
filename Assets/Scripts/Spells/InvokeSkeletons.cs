using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spells
{
    public class InvokeSkeletons : MonoBehaviour
    {
        public Transform skeleton;
        private AudioManager audioManager;
        private bool hasBeenTriggered = false;
        private int childCount;
        public event Action OnWaveStart;
        public event Action OnWaveFinished;

        private void Awake()
        {
            childCount = 0;
        }

        private void Update()
        {
            if (childCount == 0 && hasBeenTriggered)
            {
                OnWaveFinished();
            }
        }

        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        public void Invoke(int numberOfSkeletons)
        {
            if (OnWaveStart != null)
            {
                OnWaveStart();
                audioManager.Play("InvokeSkeletons");
                var position = transform.position;

                var skeletons =
                    new List<Transform> {};
                for (var i = 0; i < numberOfSkeletons; i++)
                {
                    var x = Random.Range(-2, 2);
                    var y = Random.Range(-2, 2);
                    skeletons.Add(Instantiate(skeleton, new Vector2(position.x + x, position.y + y), Quaternion.identity));
                }
                foreach (var s in skeletons)
                {
                    s.GetComponent<EnemyHealthManager>().OnDeath += HandleChildrenDeath;
                    childCount++;
                }
                
            }
            
        }

        private void HandleChildrenDeath()
        {
            childCount--;
            Debug.Log("Skeletons left " + childCount);
        }

        public void SetTrigger()
        {
            hasBeenTriggered = true;
        }
        
    }
}