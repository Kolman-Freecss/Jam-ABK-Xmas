using System.Collections;
using System.Collections.Generic;
using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._common;
using Gameplay.GameplayObjects.RoundComponents;
using UnityEngine;

public class WindowIteractable : BaseInteractableObject
{
    [SerializeField]
    private AudioClip m_chimeneySound;
    private HouseController house;

    protected override void Awake()
    {
        base.Awake();
        house = GetComponentInParent<HouseController>();
    }

    public override void DoInteraction<TData>(TData obj)
    {
        RoundManager.Instance.OnPlayerInteractsWithHouse(house);
        base.DoInteraction(obj);
    }
}
