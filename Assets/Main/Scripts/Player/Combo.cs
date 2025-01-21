using UnityEngine;

namespace Main.Scripts.Player
{
    public class Combo : MonoBehaviour
    {
        private static int combo;
        [SerializeField] private float reset = 5f;
        private static float timer;
    
        // Start is called before the first frame update
        void Start()
        {
            timer = 0f;
            combo = 0;
        }

        private void Update()
        {
            timer = timer + Time.deltaTime;
            if (timer > reset)
            {
                combo = 0;
                timer = 0f;
                Debug.Log(combo);
            }
        }

        public static void ComboIncrease()
        {
            combo++;
            timer = 0f;
            Debug.Log(combo);
        }
    }
}
