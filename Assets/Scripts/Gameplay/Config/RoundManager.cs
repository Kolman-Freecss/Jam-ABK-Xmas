#region

using System;
using System.Collections.Generic;
using Systems.NarrationSystem.Dialogue.Components;
using Systems.NarrationSystem.Dialogue.Data;
using Systems.NarrationSystem.Flow;
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
            Starting,
            Started,
            Ended
        }

        #region Inspector Variables

        public List<EnemyStateManager> enemiesInScene = new List<EnemyStateManager>();

        [SerializeField]
        private Dialogue m_RoundStartDialogue;

        #endregion

        #region Member Variables

        private RoundState m_CurrentRoundState;

        public Action OnRoundStarted;

        public static RoundManager Instance { get; private set; }

        #endregion

        #region InitData

        private void Awake()
        {
            m_CurrentRoundState = RoundState.NotStarted;
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
            m_CurrentRoundState = RoundState.Starting;
            if (m_RoundStartDialogue != null)
            {
                InitNarrationRound();
            }
            else
            {
                Debug.LogError("RoundManager: No round start dialogue set");
                StartRound();
            }
        }

        #endregion

        #region Logic

        private void InitNarrationRound()
        {
            Time.timeScale = 0f;

            DialogueInstigator.Instance.FlowChannel.OnFlowStateChanged += OnFlowStateChanged;
            DialogueInstigator.Instance.DialogueChannel.RaiseRequestDialogue(m_RoundStartDialogue);
        }

        private void OnFlowStateChanged(FlowState state)
        {
            DialogueInstigator.Instance.FlowChannel.OnFlowStateChanged -= OnFlowStateChanged;
            OnStartRound();
        }

        /// <summary>
        /// Is called by the RoundManagerUI when the player clicks on the start round button.
        /// </summary>
        public void OnStartRound()
        {
            StartRound();
        }

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
            OnRoundStarted?.Invoke();
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
