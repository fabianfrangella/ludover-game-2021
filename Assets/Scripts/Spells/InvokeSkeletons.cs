using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Spells
{
    public class InvokeSkeletons : MonoBehaviour
    {
        public Transform skeleton;
        private AudioManager audioManager;
        private int childCount;

        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        public void Invoke()
        {
            audioManager.Play("InvokeSkeletons");
            var position = transform.position;

            var skeletons =
                new List<Transform>
                {
                    Instantiate(skeleton, new Vector2(position.x + 1, position.y + 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1, position.y - 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x + 1, position.y - 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1, position.y + 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x + 1.5f, position.y + 1.5f), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1.5f, position.y - 1.5f), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x + 1.5f, position.y - 1.5f), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1.5f, position.y + 1.5f), Quaternion.identity)
                };
            foreach (var s in skeletons)
            {
                s.GetComponent<EnemyHealthManager>().OnDeath += HandleChildrenDeath;
            }

            childCount = skeletons.Count;
        }

        private void HandleChildrenDeath()
        {
            childCount--;
        }
        
        public bool AreChildrenAlive()
        {
            return childCount > 0;
        }
    }
}