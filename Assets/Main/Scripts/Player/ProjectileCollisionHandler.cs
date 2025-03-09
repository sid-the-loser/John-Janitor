using Unity.VisualScripting;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class ProjectileCollisionHandler : MonoBehaviour
    {
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}