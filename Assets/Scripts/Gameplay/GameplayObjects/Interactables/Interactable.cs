#region

using Gameplay.Config;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Gameplay.GameplayObjects.Interactables
{
    /// <summary>
    /// Base class for all interactable objects.
    /// </summary>
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        #region Member Variables

        [SerializeField]
        protected UnityEvent<object> m_OnInteraction;

        [HideInInspector]
        public bool m_IsInteractable = true;

        #endregion

        public virtual void DoInteraction<TObject>(TObject obj)
            where TObject : IInteractable
        {
            m_OnInteraction.Invoke(obj);
        }

        private void OnDestroy()
        {
            RemoveFromPlayerInteractables();
            m_OnInteraction.RemoveAllListeners();
        }

        private void OnDisable()
        {
            RemoveFromPlayerInteractables();
        }

        private void RemoveFromPlayerInteractables()
        {
            if (GameManager.Instance.m_player)
            {
                GameManager.Instance.m_player.PlayerInteractionInstigator.OnDestroyInteractable(this);
            }
        }
    }
}
