#region

using Gameplay.GameplayObjects.Interactables._common;
using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class DoorInteractable : BaseInteractableObject
    {
        [SerializeField]
        private AudioClip m_DoorOpenSound;

        public override void DoInteraction<TData>(TData obj)
        {
            // Audio.PlaySound("door_open");
            base.DoInteraction(obj);
        }
    }
}
