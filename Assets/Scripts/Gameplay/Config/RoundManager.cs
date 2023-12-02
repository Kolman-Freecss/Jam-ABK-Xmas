#region

using UnityEngine;

#endregion

namespace Gameplay.Config
{
    /// <summary>
    /// Manages the round state and the round flow.
    /// </summary>
    public class RoundManager : MonoBehaviour
    {
        public enum RoundState
        {
            NotStarted,
            Started,
            Ended
        }

        #region Member Variables

        private RoundState m_CurrentRoundState;

        public static RoundManager Instance { get; private set; }

        #endregion

        #region InitData

        private void Awake()
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
            }
        }

        private void Start()
        {
            m_CurrentRoundState = RoundState.NotStarted;
        }

        #endregion

        #region Logic

        public void DialogueStarted()
        {
            Time.timeScale = 1f;
        }

        public void DialogueEnded()
        {
            Time.timeScale = 0f;
        }

        public void StartRound()
        {
            m_CurrentRoundState = RoundState.Started;
        }

        public void EndRound()
        {
            m_CurrentRoundState = RoundState.Ended;
        }

        #endregion

        #region Getter & Setters

        public RoundState CurrentRoundState => m_CurrentRoundState;

        #endregion
    }
}
