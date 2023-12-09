#region

using System;
using System.Collections;
using Gameplay.GameplayObjects.Character.Player;
using UnityEngine;

#endregion

namespace Gameplay.Config
{
    public class GameManager : MonoBehaviour
    {
        public enum RoundTypes
        {
            InGame_City,
        }

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
            SoundManager.Instance.StartBackgroundMusic(SoundManager.BackgroundMusic.InGame_City);
            IsGameStarted = true;
            OnGameStarted?.Invoke();
        }

        public void EndGame()
        {
            IsGameStarted = false;

            StartCoroutine(EndGameCoroutine());

            IEnumerator EndGameCoroutine()
            {
                yield return new WaitForSeconds(4f);
                SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.EndGame);
                SoundManager.Instance.StartBackgroundMusic(SoundManager.BackgroundMusic.EndGame);
            }
        }

        public void RestartGame()
        {
            StartGame();
        }

        public void OnPlayerEndRound(RoundTypes roundType)
        {
            switch (roundType)
            {
                case RoundTypes.InGame_City:
                    EndGame();
                    break;
            }
        }

        #endregion
    }
}
