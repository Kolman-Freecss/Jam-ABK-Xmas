#region

using System;
using System.Collections.Generic;
using Gameplay.GameplayObjects.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace Gameplay.GameplayObjects.Character.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInteractionInstigator : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Input Actions")]
        [SerializeField]
        private InputActionReference m_InteractAction;

        #endregion

        #region Member Variables

        private event Action<IInteractable> m_OnDiscoverInteractable;
        private List<IInteractable> m_NearbyInteractables = new();

        #endregion

        #region Init Data

        private void OnEnable()
        {
            m_InteractAction.action.Enable();
        }

        private void Start()
        {
            m_InteractAction.action.performed += ctx => OnInteract();
            m_OnDiscoverInteractable += OnDiscoverInteractable;
        }

        #endregion

        #region Logic

        private void OnInteract()
        {
            if (HasNearbyInteractables())
            {
                //Ideally, we'd want to find the best possible interaction (ex: by distance & orientation).
                m_NearbyInteractables[0].DoInteraction(m_NearbyInteractables[0]);
            }
        }

        private void OnDiscoverInteractable(IInteractable obj)
        {
            //TODO: Animation here like a "!" above the player's head
            Debug.Log("Discovered interactable: " + obj);
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                m_NearbyInteractables.Add(interactable);
                m_OnDiscoverInteractable?.Invoke(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                m_NearbyInteractables.Remove(interactable);
            }
        }

        #region Getter & Setter

        public bool HasNearbyInteractables()
        {
            return m_NearbyInteractables.Count != 0;
        }

        #endregion

        #region Destructor

        private void OnDisable()
        {
            m_InteractAction.action.performed -= ctx => OnInteract();
        }

        #endregion
    }
}
