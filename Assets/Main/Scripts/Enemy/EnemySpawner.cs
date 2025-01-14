using System;
using UnityEngine;

namespace Sid.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab; // temporary
        [SerializeField] private Transform[] spawnLocation; // temporary
        [SerializeField] private GameObject cardPrefab;

        private bool _enemiesSpawned = false;


        private void SpawnEnemies(int index)
        {
            _enemiesSpawned = true;
            Instantiate(enemyPrefab, spawnLocation[index]);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_enemiesSpawned)
            {
                cardPrefab.SetActive(true);
                for (int i = 0; i < 10; i++)
                {
                    SpawnEnemies(0);
                }
            }
        }
    }
}
