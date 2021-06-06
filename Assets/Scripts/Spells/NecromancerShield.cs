using System;
using UnityEngine;

namespace Spells
{
    public class NecromancerShield : MonoBehaviour
    {
        public Transform shieldPrefab;

        private EnemyHealthManager enemyHealthManager;
        private Transform currentShield;

        private void Start()
        {
            enemyHealthManager = GetComponent<EnemyHealthManager>();
        }

        private void OnDestroy()
        {
            enemyHealthManager.SetAbsorption(0);
        }

        public void DestroyShield()
        {
            Destroy(gameObject);
        }

        public void ActivateShield()
        {
            enemyHealthManager.SetAbsorption(1000);
            currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }

    }
}