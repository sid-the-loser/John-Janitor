using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Main.Scripts.Common
{
    public class LevelGeneration : MonoBehaviour
    {
        [SerializeField] private GameObject[] levelPrefabs;
        private int enemyCounter;
        private int randomLevel;

        private Queue<Scene> loadedScenes = new Queue<Scene>();
        private int levelIndex = 1;
        /*
        -when all enemies in current level are dead
        -get current position
        -get a random number from the level prefab array
        -create that prefab away from the current level
        -update position
        check if the player has entered the new level
        delete the old level
        */


        void Update()
        {
            enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (PlayerIsNearEndOfCurrentLevel())
            {
                levelIndex = GlobalVariables.NextLevelIndex;
                LoadNextLevelSegment();
            }
        }

        bool PlayerIsNearEndOfCurrentLevel()
        {
            bool enemyCheck = false;
            if (enemyCounter <= 1) { enemyCheck = true;}
            return enemyCheck;
        }

        void LoadNextLevelSegment()
        {
            Scene newScene = SceneManager.CreateScene("Level_" + levelIndex);
            SceneManager.SetActiveScene(newScene);
            Instantiate(levelPrefabs[RandomLevelGrabber()], Vector3.zero, Quaternion.identity);

            loadedScenes.Enqueue(newScene);
            if (loadedScenes.Count > 2) // Assuming you want to keep only a certain number of segments loaded
            {
                SceneManager.UnloadSceneAsync(loadedScenes.Dequeue());
            }

            GlobalVariables.NextLevelIndex++;
        }
        
        private int RandomLevelGrabber()
        {
            return Random.Range(0, levelPrefabs.Length);
        }
    }
}