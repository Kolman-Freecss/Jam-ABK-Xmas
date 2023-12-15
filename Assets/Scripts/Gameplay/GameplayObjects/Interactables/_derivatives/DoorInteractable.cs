#region

using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._common;
using Gameplay.GameplayObjects.RoundComponents;
using Systems.NarrationSystem.Dialogue;
using Systems.NarrationSystem.Dialogue.Data;
using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class DoorInteractable : BaseInteractableObject
    {
        [SerializeField]
        private AudioClip m_DoorOpenSound;

        [SerializeField]
        private DialogueChannel m_dialogueChannel;

        [SerializeField]
        private Dialogue m_dialogue;

        private HouseController house;

        protected override void Awake()
        {
            base.Awake();
            house = GetComponentInParent<HouseController>();
        }

        public override void DoInteraction<TData>(TData obj)
        {
            if (m_AudioSource != null && m_DoorOpenSound != null)
            {
                m_AudioSource.clip = m_DoorOpenSound;
                m_AudioSource.Play();
            }
            RoundManager.Instance.CurrentHouse = house;
            base.DoInteraction(obj);
            RoundManager.Instance.OnPlayerCallHouse(m_dialogueChannel, m_dialogue, this);
        }
    }
}
