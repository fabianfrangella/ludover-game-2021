using System;
using UnityEngine;

namespace Enemy
{
    public class EndGameManager : MonoBehaviour
    {
        public event Action OnGameEnd;

        private bool gameEnded = false;
        private EnemyHealthManager enemyHealthManager;

        private void Start()
        {
            enemyHealthManager = GetComponent<EnemyHealthManager>();
        }

        private void Update()
        {
            if (!gameEnded && !enemyHealthManager.IsAlive() && OnGameEnd != null)
            {
                Debug.Log("Game finished");
                gameEnded = true;
                OnGameEnd();
            }
        }
    }
}