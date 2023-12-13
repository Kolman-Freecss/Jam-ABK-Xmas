﻿#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities._Utils_;
using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._derivatives;
using Gameplay.GameplayObjects.RoundComponents;
using TMPro;
using UnityEngine;

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
        private TextMeshProUGUI m_IncrementScoreText;

        [SerializeField]
        private TextMeshProUGUI m_PresentsScoreText;

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
        private Dictionary<PresentType, List<GameObject>> m_presentPrefabs = new();
        private PlayerInteractionInstigator m_playerInteractionInstigator;

        #endregion

        #region Init Data

        private void Awake()
        {
            m_playerController = GetComponent<PlayerController>();
            m_playerInteractionInstigator = GetComponent<PlayerInteractionInstigator>();
            PresentType[] presentTypes = (PresentType[])Enum.GetValues(typeof(PresentType));
            string path = "";
            foreach (PresentType presentType in presentTypes)
            {
                path = "DynamicAssets/Prefabs/Presents/" + presentType;
                m_presentPrefabs.Add(presentType, Resources.LoadAll<GameObject>(path).ToList());
            }
        }

        private void Start()
        {
            m_PresentsScoreText.text =
                RoundManager.Instance.PresentsScore.ToString() + "/" + RoundManager.Instance.PresentsToFinishRound;
            SetPlayerCostume(PlayerCostumeType.Krampus);
            m_IncrementScoreText.gameObject.SetActive(false);
        }

        #endregion

        #region Grab Present Logic

        /// <summary>
        /// Called when the player grabs a present. (This is called from the PresentInteractable)
        /// </summary>
        public void OnPresentGrab(PresentInteractable present)
        {
            try
            {
                // Sum the present value to the player's score
                RoundManager.Instance.OnPresentGrabbed(present);
                m_PresentsScoreText.text =
                    RoundManager.Instance.PresentsScore.ToString() + "/" + RoundManager.Instance.PresentsToFinishRound;
                m_IncrementScoreText.gameObject.SetActive(true);
                //RoundManager.Instance.m_playerScore += present.PresentValue;
                Destroy(present);
                GameObject birch = Instantiate(
                    m_presentPrefabs.GetValueOrDefault(PresentType.Birch)[0],
                    present.transform.position,
                    Quaternion.identity
                );

                StartCoroutine(IncrementScore());
                //TODO: Some sound here
            }
            catch (Exception e)
            {
                Debug.LogError("Error Grabbing Present -> " + e);
            }

            IEnumerator IncrementScore()
            {
                yield return new WaitForSeconds(1f);
                m_IncrementScoreText.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Costume Logic

        private void SetPlayerCostume(PlayerCostumeType costumeType)
        {
            if (m_playerCurrentCostume != null)
            {
                m_playerCurrentCostume.SetActive(false);
            }
            m_playerCurrentCostume = m_playerCostumes.Find(x => x.Key == costumeType).Value;
            m_playerCurrentCostume.SetActive(true);
        }

        public void OnPlayerCallHouse(object obj)
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
    }
}
