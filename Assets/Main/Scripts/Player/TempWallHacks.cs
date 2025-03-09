using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class TempWallHacks : MonoBehaviour
    {
        private int cooldown;
        private float cdTimer;
        private float timeActive;

        private int layerNumber;

        private List<GameObject> enemies = new List<GameObject>();
        private List<GameObject> enemyChildren = new List<GameObject>();

        void Start()
        {
            cooldown = 20;
            timeActive = 2.5f;
            cdTimer = cooldown;
            
            layerNumber = LayerMask.NameToLayer("holdLayer");
        }

        private void Update()
        {
            if (cdTimer > 0)
            {
                cdTimer -= Time.deltaTime;
            }
            
            if (Input.GetKeyDown(KeyCode.X) && cdTimer <= 0)
            {
                FindEnemies();
                StartCoroutine(ChangeLayers());
            }
        }

        private void FindEnemies()
        {
            enemies.Clear();
            enemyChildren.Clear();

            GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemiesInScene)
            {
                enemies.Add(enemy);

                foreach (Transform child in enemy.transform)
                {
                    enemyChildren.Add(child.gameObject);
                }
            }
        }

        private IEnumerator ChangeLayers()
        {
            foreach (GameObject child in enemyChildren)
            {
                child.layer = layerNumber;
            }
            
            yield return new WaitForSeconds(timeActive);
            
            foreach (GameObject child in enemyChildren)
            {
                child.layer = 0;
            }
            cdTimer = cooldown;
        }
    }
}