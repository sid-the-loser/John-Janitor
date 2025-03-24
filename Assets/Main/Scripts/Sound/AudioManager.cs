using System;
using FMOD.Studio;
using FMODUnity;
using Sound.Scripts.Sound;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Main.Scripts.Sound
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
        private GameObject _player;
        private EventInstance _musicEventInstance;
        
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            InitializeMusic(FmodEvents.Instance.ElevatorMusic);
            Instance = this;
            if (Instance == null)
            {
                Debug.LogError("More then one audio manager");
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }
        
        public StudioEventEmitter InitializeEventEmitter(GameObject emitterGameObject)
        {
            StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
            return emitter;
        }
        public EventInstance CreateEventInstance(EventReference eventReference)
        {
            Getplayer();
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstance.set3DAttributes(_player.transform.position.To3DAttributes());
            return eventInstance;
        }

        public void InitializeMusic(EventReference musicReference)
        {
            _musicEventInstance = CreateEventInstance(musicReference);
            _musicEventInstance.start();
        }
        
        public void StopMusic()
        {
            var musicBus = RuntimeManager.GetBus("bus:/Music");
            musicBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        public void SetMusicParameter(string parameterName, float parameterValue)
        {
            _musicEventInstance.setParameterByName(parameterName, parameterValue);
        }

        private void Getplayer()
        {
            if (!_player)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}
