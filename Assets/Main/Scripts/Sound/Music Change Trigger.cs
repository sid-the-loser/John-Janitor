using System.Collections.Generic;
using UnityEngine;

namespace Sound.Scripts.Sound
{
    public class MusicChangeTrigger : MonoBehaviour
    {
        public static int enemyCounter;
        private readonly List<GameObject> _enemiesInTrigger = new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemiesInTrigger.Add(other.gameObject);
                enemyCounter++;
                AudioManager.Instance.SetMusicParameter("Number of Enemies", enemyCounter);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemiesInTrigger.Remove(other.gameObject);
                enemyCounter--;
                AudioManager.Instance.SetMusicParameter("Number of Enemies", enemyCounter);
            }
        }
        private void Update()
        {
            for (int i = _enemiesInTrigger.Count - 1; i >= 0; i--)
            {
                if (!_enemiesInTrigger[i])
                {
                    _enemiesInTrigger.RemoveAt(i);
                    enemyCounter--;
                    AudioManager.Instance.SetMusicParameter("Number of Enemies", enemyCounter);
                }
            }
        }
    }
}