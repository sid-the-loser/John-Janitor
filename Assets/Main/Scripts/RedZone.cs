using UnityEngine;

namespace Sound.Scripts
{
    public class RedZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Camera.main.GetComponent<ScreenCameraShader>().ColorLUTShader.SetColor("_myColor", Color.red);
                Camera.main.GetComponent<ScreenCameraShader>().ColorLUTShader.SetFloat("_Contribution", 0.128f);
            }
        }
    }
}
