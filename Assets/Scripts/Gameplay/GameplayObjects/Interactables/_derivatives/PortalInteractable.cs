using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class PortalInteractable : BaseInteractableObject
    {
        [SerializeField]
        private AudioClip m_PortalInteraction;

        public override void DoInteraction<TData>(TData obj)
        {
            // Audio.PlaySound("PortalInteraction");
            base.DoInteraction(obj);
        }
    }
}
