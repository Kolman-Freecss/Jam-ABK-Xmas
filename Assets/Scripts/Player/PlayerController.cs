#region

using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Gameplay.GameplayObjects.Character._common.CharacterController;

#endregion

namespace Player
{
    public class PlayerController : CharacterController
    {
        public enum MovementMode
        {
            RelativeToCharacter,
            RelativeToCamera
        };

        public enum OrientationMode
        {
            OrientateToCameraForward,
            OrientateToMovementForward,
            OrientateToTarget
        };

        #region Inspector Variables

        [Header("Movement Inputs")] [SerializeField]
        private InputActionReference move;

        [SerializeField] private InputActionReference jump;

        [SerializeField] private InputActionReference sprint;

        [SerializeField] private InputActionReference crouch;

        [Header("Orientation Settings")] [SerializeField]
        float angularSpeed = 360f;

        [SerializeField] private Transform orientationTarget;
        [SerializeField] private OrientationMode orientationMode = OrientationMode.OrientateToMovementForward;

        [Header("Movement Settings")] [SerializeField]
        private MovementMode movementMode = MovementMode.RelativeToCamera;

        #endregion

        #region Member Variables

        private float verticalVelocity = 0f;
        private Vector3 velocityToApply = Vector3.zero; // World

        #endregion

        #region Init Data

        private void Awake()
        {
            base.AssignAnimationIDs();
            GetComponentReferences();
        }

        protected override void GetComponentReferences()
        {
            base.GetComponentReferences();
        }

        private void OnEnable()
        {
            move.action.Enable();
            jump.action.Enable();
            sprint.action.Enable();
            crouch.action.Enable();
        }

        #endregion

        #region Loop

        void Update()
        {
            velocityToApply = Vector3.zero;
            UpdateMovementOnPlane();
            UpdateVerticalMovement();
            m_characterController.Move(velocityToApply * Time.deltaTime);
            UpdateOrientation();
            UpdatePlayerAnimation();
            GroundCheck();
        }

        #endregion

        #region Logic

        private void UpdatePlayerAnimation()
        {
            base.UpdateAnimation(velocityToApply, verticalVelocity, jumpSpeed, m_isGrounded);
        }

        private void UpdateMovementOnPlane()
        {
            Vector2 rawMoveValue = move.action.ReadValue<Vector2>();
            Vector3 xzMoveValue = (Vector3.right * rawMoveValue.x) + (Vector3.forward * rawMoveValue.y);

            switch (movementMode)
            {
                case MovementMode.RelativeToCharacter:
                    UpdateMovementRelativeToCharacter(xzMoveValue);
                    break;
                case MovementMode.RelativeToCamera:
                    UpdateMovementRelativeToCamera(xzMoveValue);
                    break;
            }

            void UpdateMovementRelativeToCamera(Vector3 xzMoveValue)
            {
                Transform cameraTransform = Camera.main.transform;
                Vector3 xzMoveValueFromCamera = cameraTransform.TransformDirection(xzMoveValue);
                float originalMagnitude = xzMoveValueFromCamera.magnitude;
                xzMoveValueFromCamera = Vector3.ProjectOnPlane(xzMoveValueFromCamera, Vector3.up).normalized *
                                        originalMagnitude;
                Vector3 velocity = xzMoveValueFromCamera * planeSpeed;
                velocityToApply += velocity;
            }

            void UpdateMovementRelativeToCharacter(Vector3 xzMoveValue)
            {
                Vector3 velocity = xzMoveValue * planeSpeed;
                velocityToApply += velocity;
            }
        }

        private void UpdateVerticalMovement()
        {
            if (m_isGrounded)
            {
                verticalVelocity = 0f;
            }

            bool mustJump = jump.action.WasPerformedThisFrame();

            if (!m_isGrounded)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }

            if (mustJump && m_isGrounded)
            {
                verticalVelocity = jumpSpeed;
            }

            velocityToApply += Vector3.up * verticalVelocity;
        }

        private void UpdateOrientation()
        {
            if (velocityToApply.sqrMagnitude == 0f)
            {
                return;
            }

            Vector3 desiredDirection = Vector3.zero;
            switch (orientationMode)
            {
                case OrientationMode.OrientateToCameraForward:
                    desiredDirection = Camera.main.transform.forward;
                    break;
                case OrientationMode.OrientateToMovementForward:
                    if (velocityToApply.sqrMagnitude > 0f)
                    {
                        desiredDirection = velocityToApply.normalized;
                    }

                    break;
                case OrientationMode.OrientateToTarget:
                    desiredDirection = orientationTarget.transform.position - transform.position;
                    break;
            }

            float angularDistance = Vector3.SignedAngle(transform.forward, desiredDirection, Vector3.up);
            float angleToApply = angularSpeed * Time.deltaTime;
            angleToApply = Mathf.Min(angleToApply, Mathf.Abs(angularDistance));
            angleToApply *= Mathf.Sign(angularDistance);
            Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.up);
            transform.rotation = transform.rotation * rotationToApply;
        }

        #endregion

        #region Destructor

        private void OnDisable()
        {
            move.action.Disable();
            jump.action.Disable();
            sprint.action.Disable();
            crouch.action.Disable();
        }

        #endregion
    }
}