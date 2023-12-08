#region

using System;
using System.Collections.Generic;
using Data.SO.ThrowableItem;
using Gameplay.GameplayObjects.Interactables._derivatives;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

#endregion

namespace Gameplay.GameplayObjects.Character.Player._actions
{
    public class PlayerThrowAction : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Player Settings")]
        [SerializeField]
        private TextMeshProUGUI m_PlayerThrowItemsQuantityText;

        [SerializeField]
        private InputActionReference m_throwItemAction; // Left Click

        [SerializeField]
        private InputActionReference m_changeThrowItemAction; // Number key 1

        [SerializeField]
        private InputActionReference m_storeThrowItemAction; // Right click

        [Header("Player body")]
        [SerializeField]
        private GameObject m_playerRightHand;

        [Header("Throwable Settings")]
        [SerializeField]
        private float throwRange = 10f;

        [SerializeField]
        private PlayerAimingPlaneController m_aimingTarget;

        [SerializeField]
        private GameObject TempWrapper;

        #endregion

        #region Member Variables

        private List<ThrowableItem> m_throwableItems = new();
        private ThrowableItem m_currentThrowableItem;

        public Action<ThrowableItem> OnThrowItemAction;

        private PlayerInteractionInstigator m_playerInteractionInstigator;

        #endregion

        #region Init Data

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
            m_PlayerThrowItemsQuantityText.text = m_throwableItems.Count.ToString();
            m_playerInteractionInstigator = GetComponent<PlayerInteractionInstigator>();
        }

        #endregion

        #region Logic

        // <summary>
        /// Called when the player grabs a throwable item. (This is called from the ThrowableItemInteractable)
        /// </summary>
        public void OnThrowableItemGrab(ThrowableItemInteractable item)
        {
            m_throwableItems.Add(item.ThrowableItem);
            m_PlayerThrowItemsQuantityText.text = m_throwableItems.Count.ToString();
            Destroy(item.gameObject);
        }

        public void OnChangeThrowItem()
        {
            if (m_throwableItems.Count > 0)
            {
                ThrowableItem previous = m_currentThrowableItem;
                m_currentThrowableItem = m_throwableItems[0];
                if (previous != null)
                {
                    m_throwableItems.RemoveAt(0);
                    m_throwableItems.Add(previous);
                    Destroy(previous.InstancedItemPrefab);
                }
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
                Debug.Log("PlayerBehaviour: No throwable item selected");
                return;
            }
            Vector3 throwDirection = m_aimingTarget.transform.position - m_playerRightHand.transform.position;
            Vector3 playerDirection = throwDirection;
            playerDirection.y = 0;
            Quaternion q = Quaternion.LookRotation(playerDirection);
            transform.rotation = q;
            Vector3 finalShotPosition = m_playerRightHand.transform.position + (throwDirection.normalized * throwRange);
            if (
                Physics.Raycast(
                    transform.position,
                    throwDirection,
                    out RaycastHit hit,
                    throwRange,
                    m_aimingTarget.LayerMask
                )
            )
            {
                finalShotPosition = hit.point;
            }

            ThrowItemAction(m_currentThrowableItem, finalShotPosition);

            OnThrowItemAction?.Invoke(m_currentThrowableItem);

            void ThrowItemAction(ThrowableItem throwableItem, Vector3 finalShotPosition)
            {
                ThrowableObject to = m_currentThrowableItem.InstancedItemPrefab.GetComponent<ThrowableObject>();
                to.ParentConstraint.constraintActive = false;
                to.ParentConstraint.enabled = false;
                to.Rigidbody.isKinematic = false;
                to.gameObject.transform.SetParent(TempWrapper.transform, true);

                Vector3 forceToApply =
                    (finalShotPosition - m_playerRightHand.transform.position).normalized * to.ThrowForce;
                forceToApply.y += 10f;
                to.Rigidbody.AddForce(forceToApply, ForceMode.Impulse);

                m_currentThrowableItem = null;
                m_throwableItems.Remove(throwableItem);
                m_PlayerThrowItemsQuantityText.text = m_throwableItems.Count.ToString();

                to.OnThrowableObjectImpact += OnThrowableItemImpact;
            }
        }

        public void OnThrowableItemImpact(ThrowableObject to)
        {
            to.Rigidbody.isKinematic = true;
            ParticleSystem effect = to.ThrowEffect;
            effect.Play();
            Destroy(to.gameObject, to.ThrowEffectDuration);
        }

        private void ShowThrowableItem(bool show)
        {
            if (m_currentThrowableItem != null)
            {
                if (show)
                {
                    m_currentThrowableItem.InstancedItemPrefab = Instantiate(
                        m_currentThrowableItem.ItemPrefab,
                        m_playerRightHand.transform
                    );
                    ParentConstraint parentConstraint = m_currentThrowableItem
                        .InstancedItemPrefab
                        .GetComponent<ParentConstraint>();
                    if (parentConstraint == null)
                    {
                        throw new Exception("PlayerBehaviour: ParentConstraint of throwable is null");
                    }
                    parentConstraint.AddSource(
                        new ConstraintSource() { sourceTransform = m_playerRightHand.transform, weight = 1 }
                    );
                    parentConstraint.constraintActive = true;
                    parentConstraint.enabled = true;
                }
                else
                {
                    Destroy(m_currentThrowableItem.InstancedItemPrefab);
                    m_currentThrowableItem = null;
                }
            }
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
