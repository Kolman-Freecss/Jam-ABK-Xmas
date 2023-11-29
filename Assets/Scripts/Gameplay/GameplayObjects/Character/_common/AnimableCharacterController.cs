#region

using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character._common
{
    public class AnimableCharacterController : MonoBehaviour
    {
        #region Inspector Variables

        [SerializeField] protected Animator m_animator;

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
            TryGetComponent(out m_animator);
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