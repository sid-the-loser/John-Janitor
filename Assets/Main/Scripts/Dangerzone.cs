using Sid.Scripts.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sid.Temp.Scripts
{
    public class Dangerzone : MonoBehaviour
    {
        private int _countOfEnemy;
    
        private void FixedUpdate()
        {
            _countOfEnemy = FindObjectsOfType<BasicMeleeEnemy>().Length;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Camera.main.GetComponent<ScreenCameraShader>().ColorLUTShader.SetColor("_myColor", Color.white);
                Camera.main.GetComponent<ScreenCameraShader>().ColorLUTShader.SetFloat("_Contribution", 0.0002f);
            }
            else if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                if (_countOfEnemy-1 <= 0)
                {
                    SceneManager.LoadScene("PlaceHolder");
                }
            
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
