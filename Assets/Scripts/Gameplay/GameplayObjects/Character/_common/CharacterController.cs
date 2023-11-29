#region

using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character._common
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public abstract class CharacterController : AnimableCharacterController
    {
        #region Inspector Variables

        [Header("Movement Settings")] [Tooltip("Movement speed of the player")] [SerializeField]
        protected float planeSpeed = 3f; // m/s

        [Tooltip("Sprint speed of the player")] [SerializeField]
        protected float sprintSpeed = 12f;

        [Tooltip("Speed crouch of the Character")] [SerializeField]
        protected float crouchSpeed = 3f;

        [Tooltip("How fast the character turns to face movement direction")] [Range(0.0f, 0.3f)]
        public float rotationSmoothTime = 0.12f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float fallTimeout = 0.15f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float gravity = -9.8f;

        [Header("Player Grounded")] [Tooltip("What layers the character uses as ground")]
        public LayerMask groundLayers;

        [Tooltip("Useful for rough ground")] public float groundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float groundedRadius = 0.28f;

        [Tooltip("Jump Height")] public float jumpHeight = 1.2f;

        [Tooltip("Jump Speed")] public float jumpSpeed = 8f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float jumpTimeout = 0.50f;

        #endregion

        #region Member Variables

        protected UnityEngine.CharacterController m_characterController;

        #endregion

        #region Init Data

        protected override void GetComponentReferences()
        {
            base.GetComponentReferences();
            m_characterController = GetComponent<UnityEngine.CharacterController>();
        }

        #endregion

        #region Logic

        protected void GroundCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
                transform.position.z);
            m_isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
                QueryTriggerInteraction.Ignore);
            if (HasAnimator)
            {
                Animator.SetBool(AnimIDIsGrounded, m_isGrounded);
            }
        }

        #endregion

        #region Event Functions

        protected void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (m_isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
                groundedRadius);
        }

        #endregion

        #region Getter & Setters

        #endregion
    }
}