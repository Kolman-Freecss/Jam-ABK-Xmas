#region

using System;
using System.Collections.Generic;
using Gameplay.GameplayObjects.RoundComponents;
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
        private HouseController m_CurrentHouse;
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
            try
            {
                Time.timeScale = 0f;
                DialogueInstigator.Instance.FlowChannel.OnFlowStateChanged += OnFlowStateChanged;
                DialogueInstigator.Instance.DialogueChannel.RaiseRequestDialogue(m_RoundStartDialogue);
            }
            catch (Exception e)
            {
                Debug.LogError("RoundManager: Error while initializing narration round: " + e);
            }
            finally
            {
                Time.timeScale = 1f;
            }
        }

        private void OnFlowStateChanged(FlowState state)
        {
            DialogueInstigator.Instance.FlowChannel.OnFlowStateChanged -= OnFlowStateChanged;
            OnStartRound();
        }

        public void OnPlayerEnterHouse(HouseController houseController)
        {
            m_CurrentHouse = houseController;
            Debug.Log("Player entered house");
        }

        public void OnPlayerExitHouse(HouseController houseController)
        {
            m_CurrentHouse = null;
            Debug.Log("Player exited house");
        }

        public void OnPlayerCompleteHouse(HouseController houseController)
        {
            //TODO: Trigger VFX.
            Debug.Log("Player completed house");
        }

        public void OnPlayerFailHouse(HouseController houseController)
        {
            //TODO: Trigger VFX and sound.
            Debug.Log("Player failed house");
        }

        public void OnParentKillers(HouseController houseController)
        {
            Debug.Log("Parent killers");
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

        public void OnGameOver()
        {
            GameManager.Instance.EndGame();
        }

        #endregion

        #region Getter & Setters

        public RoundState CurrentRoundState => m_CurrentRoundState;

        public HouseController CurrentHouse => m_CurrentHouse;

        #endregion
    }
}
