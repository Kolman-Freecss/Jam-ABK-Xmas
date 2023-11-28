#region

using System;
using UnityEngine;

#endregion

namespace Gameplay.Config
{
    public class GameManager : MonoBehaviour
    {
        #region Member properties

        public static GameManager Instance { get; private set; }

        public event Action OnGameStarted;

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
            OnGameStarted?.Invoke();
        }

        #endregion
    }
}