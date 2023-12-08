#region

using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character._common
{
    public abstract class AnimableCharacterController : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Animation")] [SerializeField] protected Animator m_animator;
        [SerializeField] protected float transitionVelocity = 1f;

        Vector3 smoothedAnimationVelocity = Vector3.zero;

        #endregion

        #region Member Variables

        protected bool m_isGrounded;

        protected bool m_hasAnimator;

        protected int m_animIDForwardVelocity;

        protected int m_animIDBackwardVelocity;

        protected int m_animIDNormalizedVerticalVelocity;

        protected int m_animIDIsGrounded;

        #endregion

        #region Init Data

        protected virtual void GetComponentReferences()
        {
            m_animator = GetComponentInChildren<Animator>();
            m_hasAnimator = m_animator != null;
        }

        #endregion

        #region Logic

        protected virtual void AssignAnimationIDs()
        {
            m_animIDForwardVelocity = Animator.StringToHash("ForwardVelocity");
            m_animIDBackwardVelocity = Animator.StringToHash("BackwardVelocity");
            m_animIDNormalizedVerticalVelocity = Animator.StringToHash("NormalizedVerticalVelocity");
            m_animIDIsGrounded = Animator.StringToHash("IsGrounded");
        }

        protected virtual void UpdateAnimation(Vector3 lastVelocity, float verticalVelocity, float jumpSpeed,
            bool isGrounded)
        {
            if (!m_hasAnimator)
            {
                return;
            }

            Vector3 velocityDistance = lastVelocity - smoothedAnimationVelocity;
            float transitionVelocityToApply = transitionVelocity * Time.deltaTime;
            transitionVelocityToApply = Mathf.Min(transitionVelocityToApply, velocityDistance.magnitude);

            smoothedAnimationVelocity += velocityDistance.normalized * transitionVelocityToApply;

            Vector3 localSmoothedAnimationVelocity = transform.InverseTransformDirection(lastVelocity);
            m_animator.SetFloat("SidewardVelocity", localSmoothedAnimationVelocity.x);
            m_animator.SetFloat("ForwardVelocity", localSmoothedAnimationVelocity.z);

            float clampedVerticalVelocity = Mathf.Clamp(verticalVelocity, -jumpSpeed, jumpSpeed);
            float normalizedVerticalVelocity = Mathf.InverseLerp(-jumpSpeed, jumpSpeed, clampedVerticalVelocity);

            m_animator.SetFloat("NormalizedVerticalVelocity", normalizedVerticalVelocity);
            m_animator.SetBool("IsGrounded", isGrounded);
        }

        #endregion

        #region Getters & Setters

        public Animator Animator
        {
            get => m_animator;
            set => m_animator = value;
        }

        public bool HasAnimator
        {
            get => m_hasAnimator;
            set => m_hasAnimator = value;
        }

        public int AnimIDForwardVelocity => m_animIDForwardVelocity;
        public int AnimIDBackwardVelocity => m_animIDBackwardVelocity;
        public int AnimIDNormalizedVerticalVelocity => m_animIDNormalizedVerticalVelocity;
        public int AnimIDIsGrounded => m_animIDIsGrounded;

        #endregion
    }
}