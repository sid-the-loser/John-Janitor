using FMODUnity;
using UnityEngine;

namespace Sound.Scripts.Sound
{
    public class FmodEvents : MonoBehaviour
    {
        
        [field: Header("Music")]
        [field: SerializeField] public EventReference Music { get; private set; }
        [field: SerializeField] public EventReference ElevatorMusic { get; private set; }
        
        [field: Header("Character Noises")]
        [field: SerializeField] public EventReference Walk { get; private set; }
        [field: SerializeField] public EventReference Throw { get; private set; }
        [field: SerializeField] public EventReference Swing { get; private set; }
        
        [field: Header("Injury Sounds")]
        [field: SerializeField] public EventReference DeathSound { get; private set; }
        [field: SerializeField] public EventReference HurtSounds { get; private set; }
        
        [field: Header("Ambient Sound")]
        [field: SerializeField] public EventReference Rain { get; private set; }
        [field: SerializeField] public EventReference Vents { get; private set; }
        [field: SerializeField] public EventReference ElevatorSounds { get; private set; }
        [field: SerializeField] public EventReference ElevatorOpens { get; private set; }
        
        [field: Header("Tech Sounds")]
        [field: SerializeField] public EventReference Mouse { get; private set; }
        [field: SerializeField] public EventReference[] Keyboards { get; private set; }
        
        [field: Header("UI Sounds")]
        [field: SerializeField] public EventReference CardsHover { get; private set; }
        [field: SerializeField] public EventReference CardsSelect { get; private set; } 
        
        
        public static FmodEvents Instance { get; private set; }
        

        private void Awake()
        {
            Instance = this;
            if (Instance == null)
            {
                Debug.LogError("More than 1 instance");
            }
        }
    }
}
