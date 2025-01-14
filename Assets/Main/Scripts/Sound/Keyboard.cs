using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound.Scripts.Sound
{
    public class Keyboard : MonoBehaviour
    {
        private EventReference _keyboard;
        private StudioEventEmitter _emitter;
        void Start()
        {
            int x;
            x = Random.Range(0, 2); 
            _keyboard = FmodEvents.Instance.Keyboards[x];
            gameObject.GetComponent<StudioEventEmitter>().EventReference = _keyboard;
            gameObject.GetComponent<StudioEventEmitter>().OverrideAttenuation = true;
            gameObject.GetComponent<StudioEventEmitter>().OverrideMinDistance = 0.5f;
            gameObject.GetComponent<StudioEventEmitter>().OverrideMaxDistance = 8;

            _emitter = AudioManager.Instance.InitializeEventEmitter(this.gameObject);
            _emitter.Play();
        }
    }
}
