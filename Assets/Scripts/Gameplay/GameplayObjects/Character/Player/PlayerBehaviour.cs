#region

using System;
using System.Collections;
using System.Collections.Generic;
using Data.SO.ThrowableItem;
using Entities._Utils_;
using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._derivatives;
using Gameplay.GameplayObjects.RoundComponents;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

#endregion

namespace Gameplay.GameplayObjects.Character.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        public enum PlayerCostumeType
        {
            Krampus,
            Santa
        }

        public enum PresentType
        {
            Birch,
            Coal
        }

        #region Inspector Variables

        [Header("Player Settings")]
        [SerializeField]
        private Canvas m_PlayerCanvas;

        [SerializeField]
        private InputActionReference m_throwItemAction; // Click

        [SerializeField]
        private InputActionReference m_changeThrowItemAction; // Number key 1

        [SerializeField]
        private InputActionReference m_storeThrowItemAction; // Number key 2

        [Header("Player body")]
        [SerializeField]
        private GameObject m_playerRightHand;

        [Header("Costume Settings")]
        [SerializeField]
        private List<SerializableDictionaryEntry<PlayerCostumeType, GameObject>> m_playerCostumes;

        [SerializeField]
        private GameObject m_playerCostumeEffect;

        [SerializeField]
        private AudioClip m_costumeAudioClip;

        [Obsolete("This is a dirty mockup to test the costumeType system, this should be placed into RoundManager")]
        [SerializeField]
        private float timeToChangeCostume = 4f;

        #endregion

        #region Member Variables

        private GameObject m_playerCurrentCostume;
        private PlayerController m_playerController;
        private List<ThrowableItem> m_throwableItems = new();
        private Dictionary<PresentType, GameObject> m_presentPrefabs = new();
        private ThrowableItem m_currentThrowableItem;

        public Action<ThrowableItem> OnThrowItemAction;

        #endregion

        #region Init Data

        private void Awake()
        {
            m_playerController = GetComponent<PlayerController>();
            PresentType[] presentTypes = (PresentType[])Enum.GetValues(typeof(PresentType));
            foreach (PresentType presentType in presentTypes)
            {
                m_presentPrefabs.Add(presentType, Resources.Load<GameObject>($"Prefabs/Presents/{presentType}"));
            }
        }

        private void OnEnable()
        {
            m_throwItemAction.action.Enable();
            m_changeThrowItemAction.action.Enable();
            m_storeThrowItemAction.action.Enable();
        }

        private void Start()
        {
            m_throwItemAction.action.performed += ctx => OnThrowItem();
            m_changeThrowItemAction.action.performed += ctx => OnChangeThrowItem();
            m_storeThrowItemAction.action.performed += ctx => OnStoreThrowItem();
            SetPlayerCostume(PlayerCostumeType.Krampus);
        }

        #endregion

        #region Logic

        /// <summary>
        /// Called when the player grabs a present. (This is called from the PresentInteractable)
        /// </summary>
        public void OnPresentGrab(PresentInteractable present)
        {
            // Sum the present value to the player's score
            //RoundManager.Instance.m_playerScore += present.PresentValue;
            Destroy(present.gameObject);
            GameObject birch = Instantiate(
                m_presentPrefabs.GetValueOrDefault(PresentType.Birch),
                present.transform.position,
                Quaternion.identity
            );
            //TODO: Some sound here
        }

        /// <summary>
        /// Called when the player grabs a throwable item. (This is called from the ThrowableItemInteractable)
        /// </summary>
        public void OnThrowableItemGrab(ThrowableItemInteractable item)
        {
            m_throwableItems.Add(item.ThrowableItem);
            Destroy(item.gameObject);
        }

        public void OnChangeThrowItem()
        {
            if (m_throwableItems.Count > 0)
            {
                ThrowableItem previous = m_currentThrowableItem;
                m_currentThrowableItem = m_throwableItems[0];
                m_throwableItems.RemoveAt(0);
                m_throwableItems.Add(previous);
                ShowThrowableItem(true);
            }
        }

        public void OnStoreThrowItem()
        {
            ShowThrowableItem(false);
            m_currentThrowableItem = null;
        }

        /// <summary>
        /// OnThrowItem is called when the player throws an item.
        /// This will alert the RoundManager or Enemy that the player has thrown an item.
        /// </summary>
        public void OnThrowItem()
        {
            if (m_currentThrowableItem == null)
            {
                return;
            }
            m_currentThrowableItem.ItemPrefab.GetComponent<ParentConstraint>().constraintActive = false;
            m_currentThrowableItem.ItemPrefab.GetComponent<Rigidbody>().isKinematic = false;

            OnThrowItemAction?.Invoke(m_currentThrowableItem);
        }

        private void ShowThrowableItem(bool show)
        {
            if (m_currentThrowableItem != null && show)
            {
                ParentConstraint parentConstraint = m_currentThrowableItem.ItemPrefab.GetComponent<ParentConstraint>();
                if (parentConstraint == null)
                {
                    throw new Exception("PlayerBehaviour: ParentConstraint of throwable is null");
                }
                parentConstraint.SetTranslationOffset(0, m_playerRightHand.transform.position);
                parentConstraint.constraintActive = true;
                m_currentThrowableItem.ItemPrefab.SetActive(true);
            }
            else
            {
                m_currentThrowableItem.ItemPrefab.SetActive(false);
            }
        }

        private void SetPlayerCostume(PlayerCostumeType costumeType)
        {
            if (m_playerCurrentCostume != null)
            {
                m_playerCurrentCostume.SetActive(false);
            }
            m_playerCurrentCostume = m_playerCostumes.Find(x => x.Key == costumeType).Value;
            m_playerCurrentCostume.SetActive(true);
        }

        public void OnPlayerCallHouse()
        {
            Costume(PlayerCostumeType.Santa);
        }

        private void HidePlayerCostume()
        {
            m_playerCurrentCostume.SetActive(false);
        }

        private void Costume(PlayerCostumeType costumeType)
        {
            m_playerController.enabled = false;
            HidePlayerCostume();
            if (m_playerController.EffectsAudioSource != null)
            {
                m_playerController.EffectsAudioSource.clip = m_costumeAudioClip;
                SoundManager.Instance.PlayAudioSourceEffect(m_playerController.EffectsAudioSource);
            }

            GameObject costumeEffect = Instantiate(
                m_playerCostumeEffect,
                transform.position + new Vector3(0, 1, 0),
                Quaternion.identity,
                transform
            );
            StartCoroutine(TransitionCostume());
            IEnumerator TransitionCostume()
            {
                yield return new WaitForSeconds(timeToChangeCostume);
                Destroy(costumeEffect);
                m_playerController.enabled = true;
                m_playerController.EffectsAudioSource.Stop();
                //TODO: Make some sound and animation too.
                SetPlayerCostume(costumeType);
            }
        }

        public void OnPlayerEnterHouse(HouseController house)
        {
            m_PlayerCanvas.gameObject.SetActive(false);
            house.HouseCanvas.gameObject.SetActive(true);
        }

        public void OnPlayerExitHouse(HouseController house)
        {
            m_PlayerCanvas.gameObject.SetActive(true);
            house.HouseCanvas.gameObject.SetActive(false);
        }

        #endregion

        #region Destructor

        private void OnDisable()
        {
            m_throwItemAction.action.performed -= ctx => OnThrowItem();
            m_changeThrowItemAction.action.performed -= ctx => OnChangeThrowItem();
            m_storeThrowItemAction.action.performed -= ctx => OnStoreThrowItem();
            m_throwItemAction.action.Disable();
            m_changeThrowItemAction.action.Disable();
            m_storeThrowItemAction.action.Disable();
        }

        #endregion
    }
}
