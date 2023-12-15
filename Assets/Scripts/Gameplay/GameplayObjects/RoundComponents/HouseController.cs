#region

using System.Collections;
using Gameplay.GameplayObjects.Character.Player;
using Gameplay.GameplayObjects.Interactables._derivatives;
using Puzzle;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Gameplay.GameplayObjects.RoundComponents
{
    /// <summary>
    /// Houses are the main component of a round. They contain the puzzles and the player's goal.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class HouseController : MonoBehaviour
    {
        public enum HouseConsequence
        {
            Random,
            Police,
            ParentKillers
        }

        public enum HouseFloorType
        {
            First,
            Second
        }

        #region Inspector Variable
        [Header("House Position")]
        public Transform m_HousePosition;

        [Header("House UI")]
        private Canvas m_HouseCanvas;

        private TextMeshProUGUI m_HouseNameText;

        private TextMeshProUGUI m_TimeRemainingText;

        private TextMeshProUGUI m_PresentsRemainingText;

        [Header("House Settings")]
        public int TimeToComplete = 60;

        public HouseConsequence m_houseConsequence;
        public GameObject puzzle;

        [SerializeField]
        public string HouseName;

        [SerializeField]
        private int presentsToCollect = 5;

        [Header("Events")]
        [SerializeField]
        private UnityEvent<HouseController> onPuzzleSolved;

        [SerializeField]
        private UnityEvent<HouseController> onPuzzleFailed;

        [SerializeField]
        private UnityEvent<HouseController> onHouseEnter;

        [SerializeField]
        private UnityEvent<HouseController> onHouseExit;

        [SerializeField]
        private UnityEvent<HouseController> onHouseComplete;

        [SerializeField]
        private UnityEvent<HouseController> onHouseFail;

        [SerializeField]
        private UnityEvent<HouseController> onAlertPolice;

        [SerializeField]
        private UnityEvent<HouseController> onParentKillers;

        #endregion

        #region Member Variables

        private int m_TimeRemaining;

        private int m_PresentsRemaining;

        private bool m_isPlayerInside;

        [HideInInspector]
        public HouseFloorType m_currentHouseFloorType;
        #endregion


        #region Init Data

        private void OnEnable()
        {
            m_isPlayerInside = false;
        }

        private void Start()
        {
            m_HouseCanvas = GameObject.Find("HouseCanvas").GetComponent<Canvas>();
            m_HouseNameText = GameObject.Find("HouseNameTextHouseCanvas").GetComponent<TextMeshProUGUI>();
            m_TimeRemainingText = GameObject.Find("TimeRemainingHouseCanvas").GetComponent<TextMeshProUGUI>();
            m_PresentsRemainingText = GameObject.Find("PresentsRemainingHouseCanvas").GetComponent<TextMeshProUGUI>();
            m_TimeRemaining = TimeToComplete;
            m_PresentsRemaining = presentsToCollect;
            if (m_TimeRemainingText != null)
                m_TimeRemainingText.text = m_TimeRemaining.ToString();
            if (m_PresentsRemainingText != null)
                m_PresentsRemainingText.text = m_PresentsRemaining.ToString();
            m_HouseNameText.text = HouseName;
            m_currentHouseFloorType = HouseFloorType.First;
            ShowHouseUI(false);
        }

        #endregion

        #region Loop

        private void Update()
        {
            if (m_isPlayerInside)
            {
                m_TimeRemainingText.text = m_TimeRemaining.ToString();
                m_PresentsRemainingText.text = m_PresentsRemaining.ToString();
            }
        }

        #endregion


        #region Logic

        /// <summary>
        /// </summary>
        /// <param name="floorType"></param>
        public void OnChangeFloor(HouseFloorType floorType)
        {
            m_currentHouseFloorType = floorType;
        }

        private void ShowHouseUI(bool show)
        {
            m_HouseCanvas.enabled = show;
        }

        public void OnPuzzleSolved(PuzzleController puzzle)
        {
            puzzle.OnPuzzleSolved();
            onPuzzleSolved?.Invoke(this);
        }

        public void OnPuzzleFailed(PuzzleController puzzle)
        {
            puzzle.OnPuzzleFailed();
            onPuzzleFailed?.Invoke(this);
        }

        public void OnPresentCollected(PresentInteractable present)
        {
            Destroy(present);
            m_PresentsRemaining--;
            if (m_PresentsRemaining <= 0)
            {
                OnPlayerHouseComplete();
            }
        }

        public void OnPlayerEnterHouse()
        {
            //TODO: Trigger some sound effect.
            StartCoroutine(CountdownFail(m_TimeRemaining));
            m_isPlayerInside = true;
            ShowHouseUI(true);
            onHouseEnter?.Invoke(this);
        }

        public void OnPlayerExitHouse()
        {
            //TODO: Trigger some sound effect.
            StopAllCoroutines();
            ShowHouseUI(false);
            m_isPlayerInside = false;
            onHouseExit?.Invoke(this);
        }

        public void OnPlayerHouseFail()
        {
            HouseConsequence consequence =
                m_houseConsequence != HouseConsequence.Random
                    ? m_houseConsequence
                    : Random.Range(0, 2) == 0
                        ? HouseConsequence.Police
                        : HouseConsequence.ParentKillers;
            switch (consequence)
            {
                case HouseConsequence.Police:
                    OnAlertPolice();
                    break;
                case HouseConsequence.ParentKillers:
                    OnParentKillers();
                    break;
            }

            onHouseFail?.Invoke(this);
        }

        private void OnAlertPolice()
        {
            onAlertPolice?.Invoke(this);
        }

        private void OnParentKillers()
        {
            onParentKillers?.Invoke(this);
        }

        private void OnPlayerHouseComplete()
        {
            StopAllCoroutines();
            onHouseComplete?.Invoke(this);
        }

        #endregion

        IEnumerator CountdownFail(int time)
        {
            m_TimeRemaining = time;
            while (m_TimeRemaining > 0)
            {
                yield return new WaitForSeconds(1);
                m_TimeRemaining--;
            }
            OnPlayerHouseFail();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() is { } playerController)
            {
                OnPlayerEnterHouse();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerController>() is { } playerController)
            {
                OnPlayerExitHouse();
            }
        }

        #region Destructor

        private void OnDestroy()
        {
            onHouseEnter.RemoveAllListeners();
            onHouseExit.RemoveAllListeners();
        }

        #endregion

        #region Getter & Setter

        public Canvas HouseCanvas => m_HouseCanvas;

        public void SetPuzzle(GameObject puzzle)
        {
            this.puzzle = puzzle;
            this.puzzle.GetComponent<PuzzleController>().houseController = this;
        }

        #endregion
    }
}
