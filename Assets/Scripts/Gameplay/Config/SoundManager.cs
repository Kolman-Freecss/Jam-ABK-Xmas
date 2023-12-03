#region

using System;
using System.Collections.Generic;
using Entities._Utils_;
using UnityEngine;

#endregion

namespace Gameplay.Config
{
    public class SoundManager : MonoBehaviour
    {
        [Serializable]
        public enum BackgroundMusic
        {
            Intro,
            InGame_City,
            InsideHouse,
            EndGame
        }

        #region Inspector Variables

        [Range(0, 100)]
        public float EffectsAudioVolume = 50f;

        [Range(0, 100)]
        public float MusicAudioVolume = 40f;

        [Range(0, 100)]
        public float MasterAudioVolume = 40f;

        public List<SerializableDictionaryEntry<BackgroundMusic, AudioClip>> BackgroundMusicClips;
        public AudioClip ButtonClickSound;

        #endregion

        #region Member Variables

        private AudioSource audioSource;

        public static SoundManager Instance { get; private set; }

        #endregion

        #region InitData

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            ManageSingleton();
        }

        void ManageSingleton()
        {
            if (Instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            if (BackgroundMusicClips == null)
                BackgroundMusicClips = new List<SerializableDictionaryEntry<BackgroundMusic, AudioClip>>();
            SetEffectsVolume(EffectsAudioVolume);
            SetMusicVolume(MusicAudioVolume);
            StartBackgroundMusic(BackgroundMusic.Intro);
        }

        #endregion

        #region Logic

        public void StartBackgroundMusic(BackgroundMusic backgroundMusic)
        {
            AudioClip clip = BackgroundMusicClips.Find(x => x.Key == backgroundMusic).Value;
            if (clip != null)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"No clip found for {backgroundMusic}");
            }
        }

        public void SetEffectsVolume(float volume)
        {
            EffectsAudioVolume = volume;
        }

        public void SetMusicVolume(float volume)
        {
            audioSource.volume = volume / 100;
        }
        
        public void SetMasterVolume(float volume)
        {
            AudioListener.volume = volume / 100;
        }

        public void PlayButtonClickSound(Vector3 position)
        {
            AudioSource.PlayClipAtPoint(ButtonClickSound, position, EffectsAudioVolume / 100);
        }

        #endregion
    }
}
