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

        private float m_verticalVelocity = 0f;
        private float m_rotationVelocity = 0f;
        Vector3 m_currentHorizontalSpeed = Vector3.zero;

        private float m_jumpTimeoutDelta;
        private float m_fallTimeoutDelta;
        private float m_terminalVelocity = 53.0f;

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
            Jump();
            GroundCheck();
            Move();
            UpdateRotation(m_currentHorizontalSpeed);
        }

        #endregion

        #region Logic

        void Jump()
        {
            bool mustJump = jump.action.WasPerformedThisFrame();
            if (m_isGrounded)
            {
                // reset the fall timeout timer
                m_fallTimeoutDelta = FallTimeout;

                // if (_hasAnimator)
                // {
                //     _animator.SetBool(_animIDJump, false);
                // }

                // stop our velocity dropping infinitely when grounded
                if (m_verticalVelocity < 0.0f)
                {
                    m_verticalVelocity = -2f;
                }

                // Jump
                if (mustJump && m_jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    m_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    // if (_hasAnimator)
                    // {
                    //     _animator.SetBool(_animIDJump, true);
                    // }
                }

                // jump timeout
                if (m_jumpTimeoutDelta >= 0.0f)
                {
                    m_jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                m_jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (m_fallTimeoutDelta >= 0.0f)
                {
                    m_fallTimeoutDelta -= Time.deltaTime;
                }
                // else
                // {
                //     // update animator if using character
                //     if (m_hasAnimator)
                //     {
                //         m_animator.SetBool(m_animIDFreeFall, true);
                //     }
                // }

                // if we are not grounded, do not jump
                mustJump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (m_verticalVelocity < m_terminalVelocity)
            {
                // if (_hasAnimator)
                // {
                //     _animator.SetFloat(_animIDJumpVelocity, _verticalVelocity);
                // }
                m_verticalVelocity += Gravity * Time.deltaTime;
            }

            if (HasAnimator)
            {
                Animator.SetFloat(AnimIDNormalizedVerticalVelocity, m_verticalVelocity / JumpHeight);
            }
        }

        void Move()
        {
            bool mustCrouch = crouch.action.WasPerformedThisFrame();
            bool mustSprint = false;
            if (mustCrouch)
            {
                mustSprint = false;
            }
            else
            {
                mustSprint = sprint.action.WasPerformedThisFrame();
            }

            float targetSpeed = mustSprint ? SprintSpeed : Speed;

            Vector2 rawMoveValue = move.action.ReadValue<Vector2>();

            if (rawMoveValue == Vector2.zero) targetSpeed = 0.0f;

            m_currentHorizontalSpeed =
                new Vector3(rawMoveValue.x, 0.0f, rawMoveValue.y);

            m_currentHorizontalSpeed = GetVelocityToApply(m_currentHorizontalSpeed);

            m_controller.Move(m_currentHorizontalSpeed + new Vector3(0.0f, m_verticalVelocity, 0.0f) * Time.deltaTime);

            if (HasAnimator)
            {
                Vector3 localSmoothedAnimationVelocity = transform.InverseTransformDirection(m_currentHorizontalSpeed);
                Animator.SetFloat(AnimIDForwardVelocity, localSmoothedAnimationVelocity.z);
                Animator.SetFloat(AnimIDBackwardVelocity, localSmoothedAnimationVelocity.x);
            }

            Vector3 GetVelocityToApply(Vector3 xzMoveValue)
            {
                switch (movementMode)
                {
                    case MovementMode.RelativeToCharacter:
                        return UpdateMovementRelativeToCharacter(xzMoveValue);
                    case MovementMode.RelativeToCamera:
                        return UpdateMovementRelativeToCamera(xzMoveValue);
                    default:
                        return Vector3.zero;
                }

                Vector3 UpdateMovementRelativeToCamera(Vector3 xzMoveValue)
                {
                    Transform cameraTransform = Camera.main.transform;
                    Vector3 xzMoveValueFromCamera = cameraTransform.TransformDirection(xzMoveValue);
                    float originalMagnitude = xzMoveValueFromCamera.magnitude;
                    xzMoveValueFromCamera = Vector3.ProjectOnPlane(xzMoveValueFromCamera, Vector3.up).normalized *
                                            originalMagnitude;
                    Vector3 velocity = xzMoveValueFromCamera * targetSpeed;
                    return velocity;
                }

                Vector3 UpdateMovementRelativeToCharacter(Vector3 xzMoveValue)
                {
                    Vector3 velocity = xzMoveValue * targetSpeed;
                    return velocity;
                }
            }
        }

        void UpdateRotation(Vector3 currentHorizontalSpeed)
        {
            Vector3 desiredDirection = Vector3.zero;
            switch (orientationMode)
            {
                case OrientationMode.OrientateToCameraForward:
                    desiredDirection = Camera.main.transform.forward;
                    break;
                case OrientationMode.OrientateToMovementForward:
                    if (currentHorizontalSpeed.sqrMagnitude > 0f)
                    {
                        desiredDirection = currentHorizontalSpeed.normalized;
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