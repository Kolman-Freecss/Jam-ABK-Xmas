#region

using System;
using System.Collections.Generic;
using Gameplay.GameplayObjects.Interactables._derivatives;
using Gameplay.GameplayObjects.RoundComponents;
using Systems.NarrationSystem.Dialogue.Components;
using Systems.NarrationSystem.Dialogue.Data;
using Systems.NarrationSystem.Flow;
using UnityEngine;
using UnityEngine.Events;

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

        public static RoundManager Instance { get; private set; }

        #region Inspector Variables

        [Header("Round Settings")]
        public GameManager.RoundTypes roundType = GameManager.RoundTypes.InGame_City;

        public List<EnemyStateManager> enemiesInScene = new();

        [SerializeField]
        private Dialogue m_RoundStartDialogue;

        [Header("Presents Settings")]
        public int presentsToFinishRound = 10;

        #endregion

        #region Member Variables

        // Enemy settings
        [HideInInspector]
        public UnityEvent bossCall = new UnityEvent();

        // Player state
        private int presentsScore = 0;

        // Round Settings
        private RoundState m_CurrentRoundState;
        public Action OnRoundStarted;

        // House Settings
        private HouseController m_CurrentHouse;

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
            InitRoundData();
        }

        private void InitRoundData()
        {
            presentsScore = 0;
        }

        #endregion

        #region House Flow

        public void OnPlayerEnterHouse(HouseController houseController)
        {
            m_CurrentHouse = houseController;
            GameManager.Instance.m_player.PlayerBehaviour.OnPlayerEnterHouse(houseController);
            Debug.Log("Player entered house");
        }

        public void OnPlayerExitHouse(HouseController houseController)
        {
            m_CurrentHouse = null;
            GameManager.Instance.m_player.PlayerBehaviour.OnPlayerExitHouse(houseController);
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

        #endregion

        #region Dialogue Logic

        private void InitNarrationRound()
        {
            try
            {
                DialogueInstigator.Instance.FlowChannel.OnFlowStateChanged += OnFlowStateChanged;
                DialogueInstigator.Instance.DialogueChannel.RaiseRequestDialogue(m_RoundStartDialogue);
            }
            catch (Exception e)
            {
                Debug.LogError("RoundManager: Error while initializing narration round: " + e);
            }
        }

        private void OnFlowStateChanged(FlowState state)
        {
            DialogueInstigator.Instance.FlowChannel.OnFlowStateChanged -= OnFlowStateChanged;
            OnStartRound();
        }

        public void DialogueStarted()
        {
            Time.timeScale = 0f;
            GameManager.Instance.m_player.enabled = false;
        }

        public void DialogueEnded()
        {
            Time.timeScale = 1f;
            if (GameManager.Instance.m_player == null)
                return;
            GameManager.Instance.m_player.enabled = true;
        }

        #endregion

        #region Round Flow

        /// <summary>
        /// Is called by the RoundManagerUI when the player clicks on the start round button.
        /// </summary>
        public void OnStartRound()
        {
            StartRound();
        }

        public void StartRound()
        {
            m_CurrentRoundState = RoundState.Started;
            OnRoundStarted?.Invoke();
        }

        public void EndRound()
        {
            m_CurrentRoundState = RoundState.Ended;
            GameManager.Instance.OnPlayerEndRound(roundType);
        }

        #endregion

        #region Presents Logic

        public void OnPresentGrabbed(PresentInteractable present)
        {
            presentsScore++;
            CheckPresents();
        }

        private void CheckPresents()
        {
            if (presentsScore >= presentsToFinishRound)
            {
                Instance.EndRound();
            }
        }

        #endregion

        #region Enemy Logic

        public void BossCall()
        {
            bossCall?.Invoke();
        }

        #endregion

        #region Getter & Setters

        public RoundState CurrentRoundState => m_CurrentRoundState;

        public int PresentsScore => presentsScore;

        public int PresentsToFinishRound => presentsToFinishRound;

        public HouseController CurrentHouse => m_CurrentHouse;

        #endregion
    }
}
