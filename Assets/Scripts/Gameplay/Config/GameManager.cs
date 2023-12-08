#region

using System;
using Gameplay.GameplayObjects.Character.Player;
using UnityEngine;

#endregion

namespace Gameplay.Config
{
    public class GameManager : MonoBehaviour
    {
        #region Member properties

        public static GameManager Instance { get; private set; }

        public event Action OnGameStarted;
        public bool IsGameStarted { get; private set; }

        [HideInInspector]
        public PlayerController m_player;

        #endregion

        #region InitData

        void Awake()
        {
            ManageSingleton();
        }

        private void ManageSingleton()
        {
            if (Instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        #endregion

        #region Logic

        public void StartGame()
        {
            SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.InGame_City);
            SoundManager.Instance.StartBackgroundMusic(SoundManager.BackgroundMusic.InGame_City);
            IsGameStarted = true;
            OnGameStarted?.Invoke();
        }

        public void EndGame()
        {
            SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.EndGame);
            IsGameStarted = false;
        }

        public void RestartGame()
        {
            StartGame();
        }

        #endregion
    }
}
