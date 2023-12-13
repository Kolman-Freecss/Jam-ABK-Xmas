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

        #region Inspector Variable
        [Header("House Position")]
        public Transform m_HousePosition;

        [Header("House UI")]
        [SerializeField]
        private Canvas m_HouseCanvas;

        [SerializeField]
        private TextMeshProUGUI m_HouseNameText;

        [SerializeField]
        private TextMeshProUGUI m_TimeRemainingText;

        [SerializeField]
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

        #endregion


        #region Init Data

        private void OnEnable()
        {
            m_isPlayerInside = false;
            ShowHouseUI(false);
        }

        private void Start()
        {
            m_TimeRemaining = TimeToComplete;
            m_PresentsRemaining = presentsToCollect;
            m_TimeRemainingText.text = m_TimeRemaining.ToString();
            m_PresentsRemainingText.text = m_PresentsRemaining.ToString();
            m_HouseNameText.text = HouseName;
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

        #endregion
    }
}
