#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace Gameplay.GameplayObjects.Character.Player
{
    public class PlayerAimingPlaneController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask = Physics.DefaultRaycastLayers;

        private Camera m_MainCamera;

        private void Start()
        {
            m_MainCamera = Camera.main;
        }

        void Update()
        {
            Vector2 cursorPosititon = Mouse.current.position.ReadValue(); // Screen Space
            Ray ray = m_MainCamera.ScreenPointToRay(cursorPosititon);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                transform.position = hit.point;
            }
        }

        #region Getter & Setter

        public LayerMask LayerMask => layerMask;

        #endregion
    }
}
