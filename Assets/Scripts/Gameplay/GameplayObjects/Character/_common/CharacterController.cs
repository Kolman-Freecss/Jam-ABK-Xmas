#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Gameplay.GameplayObjects.Character._common
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public abstract class CharacterController : AnimableCharacterController
    {
        #region Inspector Variables

        [Header("Audio Settings")]
        [SerializeField]
        protected AudioSource m_effectsAudioSource;

        [Header("Movement Settings")]
        [Tooltip("Movement speed of the player")]
        [SerializeField]
        protected float planeSpeed = 3f; // m/s

        [Tooltip("Sprint speed of the player")]
        [SerializeField]
        protected float sprintSpeed = 12f;

        [Tooltip("Speed crouch of the Character")]
        [SerializeField]
        protected float crouchSpeed = 3f;

        [Tooltip("Maximun stamina of the player")]
        [SerializeField]
        protected float maxStamina = 100f;

        [Tooltip("Maximun stamina of the player")]
        [SerializeField]
        protected float currentStamina;

        [Tooltip("Stamina cost when sprinting")]
        [SerializeField]
        protected float sprintStaminaCost = 20f;

        [Tooltip("Stamina recovery speed")]
        [SerializeField]
        protected float staminaRecoveryRate = 2f;

        [Tooltip("Stamina recovery speed")]
        [SerializeField]
        protected Slider staminaSlider;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float rotationSmoothTime = 0.12f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float fallTimeout = 0.15f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float gravity = -9.8f;

        [Header("Player Grounded")]
        [Tooltip("What layers the character uses as ground")]
        public LayerMask groundLayers;

        [Tooltip("Useful for rough ground")]
        public float groundedOffset = -0.20f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float groundedRadius = 0.28f;

        [Tooltip("Jump Height")]
        public float jumpHeight = 1.2f;

        [Tooltip("Jump Speed")]
        public float jumpSpeed = 8f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float jumpTimeout = 0.50f;

        #endregion

        #region Member Variables

        protected UnityEngine.CharacterController m_characterController;

        [HideInInspector]
        public float m_currentSpeed;

        #endregion

        #region Init Data

        protected void Start()
        {
            m_currentSpeed = planeSpeed;

            currentStamina = maxStamina;

            if (staminaSlider)
            {
                staminaSlider.maxValue = maxStamina;
                staminaSlider.value = currentStamina;
            }
        }

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
            Vector3 spherePosition = new Vector3(
                transform.position.x,
                transform.position.y - groundedOffset,
                transform.position.z
            );
            m_isGrounded = Physics.CheckSphere(
                spherePosition,
                groundedRadius,
                groundLayers,
                QueryTriggerInteraction.Ignore
            );
            if (HasAnimator)
            {
                Animator.SetBool(AnimIDIsGrounded, m_isGrounded);
            }
        }

        public void Hide(bool hide)
        {
            if (hide)
                StopMovement();
            else
                ResetSpeed();
            
             if (HasAnimator)
             {
                 Animator.SetBool("IsHide", hide);
             }
        }

        public void Stealth(bool stealth, float speedReduction = 0f)
        {
            if (stealth)
                ReduceSpeed(speedReduction);
            else
                ResetSpeed();
 
            if (HasAnimator)
             {
                 Animator.SetBool("IsStealth", stealth);
             }
        }

        public void ReduceSpeed(float reduction)
        {
            m_currentSpeed -= reduction;
        }

        public void ResetSpeed()
        {
            m_currentSpeed = planeSpeed;
        }

        public void StopMovement()
        {
            m_currentSpeed = 0f;
        }

        #endregion

        #region Event Functions

        protected void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (m_isGrounded)
                Gizmos.color = transparentGreen;
            else
                Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
                groundedRadius
            );
        }

        #endregion

        #region Getter & Setters

        public AudioSource EffectsAudioSource => m_effectsAudioSource;
        //public Animator animator => 

        #endregion
    }
}
