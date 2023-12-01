#region

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
        UnityEvent<object> m_OnInteraction;

        #endregion

        public virtual void DoInteraction<TObject>(TObject obj)
            where TObject : IInteractable
        {
            m_OnInteraction.Invoke(obj);
        }
    }
}
