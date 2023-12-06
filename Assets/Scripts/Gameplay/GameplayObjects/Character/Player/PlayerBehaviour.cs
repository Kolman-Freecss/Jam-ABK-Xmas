#region

using System;
using System.Collections;
using System.Collections.Generic;
using Entities._Utils_;
using Gameplay.Config;
using Gameplay.GameplayObjects.RoundComponents;
using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        public enum PlayerCostume
        {
            Krampus,
            Santa
        }

        #region Inspector Variables

        [Header("Player Settings")]
        [SerializeField]
        private Canvas m_PlayerCanvas;

        [Header("Costume Settings")]
        [SerializeField]
        private List<SerializableDictionaryEntry<PlayerCostume, GameObject>> m_playerCostumes;

        [SerializeField]
        private GameObject m_playerCostumeEffect;

        [SerializeField]
        private AudioClip m_costumeAudioClip;

        [Obsolete("This is a dirty mockup to test the costume system, this should be placed into RoundManager")]
        [SerializeField]
        private float timeToChangeCostume = 4f;

        #endregion

        #region Member Variables

        private GameObject m_playerCurrentCostume;
        private PlayerController m_playerController;

        #endregion

        #region Init Data

        private void Awake()
        {
            m_playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            SetPlayerCostume(PlayerCostume.Krampus);
        }

        private void SetPlayerCostume(PlayerCostume costume)
        {
            if (m_playerCurrentCostume != null)
            {
                m_playerCurrentCostume.SetActive(false);
            }
            m_playerCurrentCostume = m_playerCostumes.Find(x => x.Key == costume).Value;
            m_playerCurrentCostume.SetActive(true);
        }

        private void HidePlayerCostume()
        {
            m_playerCurrentCostume.SetActive(false);
        }

        public void OnPlayerCallHouse()
        {
            Costume(PlayerCostume.Santa);
        }

        public void Costume(PlayerCostume costume)
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
                SetPlayerCostume(costume);
            }
        }

        #endregion

        #region Logic

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
