using System;
using System.Collections.Generic;
using Pause;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Spawner : MonoBehaviour
    {
        public Transform character;

        public int maxAlive;
        public int alive;
        public Vector2 xRange;
        public Vector2 yRange;
        private void Start()
        {
            maxAlive = 10;
            alive = 0;
            Spawn();
        }
        private void Update()
        {
            if (PauseManager.IsGamePaused()) return;
            if (alive < maxAlive)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            while (alive < maxAlive)
            {
                var pos = new Vector2(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y));
                var spawn = Instantiate(character, pos, Quaternion.identity);
                spawn.GetComponent<EnemyHealthManager>().OnDeath += HandleDeath;
                spawn.GetComponent<EnemyPathFinder>().state = EnemyPathFinder.State.WANDERING;
                alive++;
            }
        }

        private void HandleDeath()
        {
            alive--;
        }
        
    }
}