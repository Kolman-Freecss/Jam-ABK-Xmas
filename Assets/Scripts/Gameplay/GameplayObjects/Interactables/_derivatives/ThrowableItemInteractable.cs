#region

using Data.SO.ThrowableItem;
using Gameplay.GameplayObjects.Interactables._common;
using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class ThrowableItemInteractable : BaseInteractableObject
    {
        [SerializeField]
        ThrowableItem m_ThrowableItem;

        public override void DoInteraction<TData>(TData obj)
        {
            base.DoInteraction(obj);
        }

        #region Getter & Setter

        public ThrowableItem ThrowableItem => m_ThrowableItem;

        #endregion
    }
}
