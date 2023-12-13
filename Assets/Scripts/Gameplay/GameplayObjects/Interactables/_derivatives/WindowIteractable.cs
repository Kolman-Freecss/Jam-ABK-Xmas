using System.Collections;
using System.Collections.Generic;
using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._common;
using UnityEngine;

public class WindowIteractable : BaseInteractableObject
{
    [SerializeField]
    private AudioClip m_chimeneySound;
    

    public override void DoInteraction<TData>(TData obj)
    {
        RoundManager.Instance.OnPlayerInteractsWithPuzzle();
        base.DoInteraction(obj);
    }
}
