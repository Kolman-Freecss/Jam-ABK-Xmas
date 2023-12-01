#region

using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace Gameplay.GameplayObjects.Character.Player
{
    public class PlayerStealthBehaviour : CharacterStealthBehaviour
    {
        #region Inspector Variables

        [SerializeField] InputActionReference stealth;

        [SerializeField] InputActionReference hide;

        #endregion

        #region Member Variables

        private PlayerController m_playerController;

        #endregion

        #region Init Data

        private void OnEnable()
        {
            stealth.action.Enable();
            hide.action.Enable();
        }

        protected new void Start()
        {
            base.Start();
            m_playerController = GetComponent<PlayerController>();
            // Assign events
            stealth.action.performed += ctx => OnStealth();
            hide.action.performed += ctx => OnHide();
        }

        #endregion

        #region Logic

        public override void OnDetect()
        {
            //TODO: Animation here like a "!" above the player's head
            base.OnDetect();
        }

        protected override void OnHide()
        {
            //TODO: Animation here like a "Wait me here" above the player's head
            base.OnHide();
        }

        protected override void OnStealth()
        {
            //TODO: Animation here like a "..." above the player's head
            base.OnStealth();
        }

        #endregion

        #region Destructor

        private void OnDisable()
        {
            stealth.action.Disable();
            hide.action.Disable();
        }

        #endregion
    }
}