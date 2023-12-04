#region

using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._common;
using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class PresentInteractable : BaseInteractableObject
    {
        [SerializeField]
        private AudioClip m_PresentReachedSound;

        public override void DoInteraction<TData>(TData obj)
        {
            RoundManager.Instance.CurrentHouse.OnPresentCollected(obj as PresentInteractable);
            // Audio.PlaySound("PresentReached");
            base.DoInteraction(obj);
        }
    }
}
