#region

using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._common;
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

        public override void DoInteraction<TData>(TData obj)
        {
            Time.timeScale = 0f;
            GameManager.Instance.m_player.enabled = false;
            m_AudioSource.clip = m_DoorOpenSound;
            m_AudioSource.Play();
            gameObject.SetActive(false);
            if (obj is DialogueChannel)
            {
                Debug.Log("DialogueChannel");
                //((DialogueChannel)obj).OnDialogueEnd += OnDialogueEnd;
            }
            base.DoInteraction(obj);
        }

        private void OnDialogueEnd(Dialogue dialogue)
        {
            gameObject.SetActive(true);
            Time.timeScale = 1f;
            GameManager.Instance.m_player.enabled = true;
        }
    }
}
