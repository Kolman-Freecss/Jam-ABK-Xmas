#region

using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character._common
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public class CharacterController : AnimableCharacterController
    {
        #region Inspector Variables

        [Header("Movement Settings")] [Tooltip("Movement speed of the player")] [SerializeField]
        protected float Speed = 6f;

        [Tooltip("Sprint speed of the player")] [SerializeField]
        protected float SprintSpeed = 12f;

        [Tooltip("How fast the character turns to face movement direction")] [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Header("Player Grounded")] [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Tooltip("Useful for rough ground")] public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("Jump Height")] public float JumpHeight = 1.2f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        #endregion

        #region Member Variables

        protected UnityEngine.CharacterController m_controller;

        #endregion

        #region Init Data

        protected override void GetComponentReferences()
        {
            base.GetComponentReferences();
            m_controller = GetComponent<UnityEngine.CharacterController>();
        }

        #endregion

        #region Logic

        protected void GroundCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            m_isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);
            if (HasAnimator)
            {
                m_animator.SetBool(m_animIDIsGrounded, m_isGrounded);
            }
        }

        #endregion
    }
}