#region

using Gameplay.GameplayObjects.RoundComponents;
using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Player Settings")]
        [SerializeField]
        private Canvas m_PlayerCanvas;

        #endregion

        #region Member Variables



        #endregion

        #region Init Data


        #endregion

        #region Logic

        public void OnPlayerEnterHouse(HouseController house)
        {
            m_PlayerCanvas.gameObject.SetActive(false);
            house.HouseCanvas.gameObject.SetActive(true);
        }

        public void OnPlayerExitHouse(HouseController house)
        {
            m_PlayerCanvas.gameObject.SetActive(true);
            house.HouseCanvas.gameObject.SetActive(false);
        }

        #endregion
    }
}
