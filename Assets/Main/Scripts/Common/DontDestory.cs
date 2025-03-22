using UnityEngine;

namespace Main.Scripts.Common
{
    public class DontDestory : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
