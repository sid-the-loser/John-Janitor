/*
 * For anybody working with the wave CSV files, please make sure there's more than one column in the CSV file.
 */

using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private TextAsset waveDataCSV;
    [SerializeField] private GameObject[] enemyGameObjects;
    
    private List<List<int>> waveData = new List<List<int>>();
    private int currentWave = 0;
    private int maxWave = 0;
    
    private GameObject[] spawnPoints;
    
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        
        foreach (var line in waveDataCSV.text.Split('\n'))
        {
            List<int> temp = new List<int>();
            
            if (line.Split(',').Length > 1)
            {
                foreach (var value in line.Split(','))
                {
                    temp.Add(int.Parse(value));
                }
                waveData.Add(temp);
                maxWave++;
            }
        }
        
        /*Debug.Log($"{waveData[0][0]},{waveData[0][1]},count:{waveData[0].Count}");
        Debug.Log($"{waveData[1][0]},{waveData[1][1]},count:{waveData[1].Count}");*/
    }

    public int GetCurrentWaveNumber()
    {
        return currentWave;
    }

    public GameObject[] GetCurrentWave()
    {
        List<GameObject> temp = new List<GameObject>();
        var count = 0;
        var arr = waveData[currentWave];
        
        for (int i = 0; i < arr.Count; i++)
        {
            for (int j = 0; j < arr[i]; j++)
            {
                temp.Add(enemyGameObjects[i]);
                count++;
            }
        }

        // Debug.Log(count);
        
        return temp.ToArray();
    }

    public void SpawnWave()
    {
        var gameObjects = GetCurrentWave();

        foreach (var gobs in gameObjects)
        {
            Instantiate(gobs, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        }
    }

    public bool NextWave()
    {
        currentWave++;
        if (currentWave < maxWave) // since currentWave is always 1 less than maxWave
        {
            SpawnWave();
            return true; // if there is more waves left or if it's the last wave
        }
        else
        {
            return false; // if there is no more waves left
        }
    }
}
