#region

using Gameplay.Config;
using Gameplay.GameplayObjects.Interactables._common;
using Gameplay.GameplayObjects.RoundComponents;
using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Interactables._derivatives
{
    public class StairsInteractable : BaseInteractableObject
    {
        public HouseController.HouseFloorType m_floorType;

        [SerializeField]
        private Transform m_originCoordinates;

        [SerializeField]
        private Transform m_destinationCoordinates;

        private HouseController house;

        protected override void Awake()
        {
            base.Awake();
            house = GetComponentInParent<HouseController>();
        }

        public override void DoInteraction<TData>(TData obj)
        {
            base.DoInteraction(obj);
            GameManager.Instance.m_player.gameObject.SetActive(false);
            SceneTransitionHandler.Instance.OnLoadingScene += OnTransitionFinish;
            StartCoroutine(SceneTransitionHandler.Instance.OnGameStartTransition());
        }

        void OnTransitionFinish()
        {
            GameManager.Instance.m_player.gameObject.transform.position = m_destinationCoordinates.position;
            house.OnChangeFloor(m_floorType);

            GameManager.Instance.m_player.gameObject.SetActive(true);
            SceneTransitionHandler.Instance.OnLoadingScene -= OnTransitionFinish;
        }
    }
}
