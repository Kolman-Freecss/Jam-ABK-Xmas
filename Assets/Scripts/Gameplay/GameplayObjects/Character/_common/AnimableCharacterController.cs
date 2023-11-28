#region

using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character._common
{
    public class AnimableCharacterController : MonoBehaviour
    {
        #region Inspector Variables

        [SerializeField] Animator m_animator;

        #endregion

        #region Member Variables

        private bool m_isGrounded;

        private bool m_hasAnimator;

        private int m_animIDForwardVelocity;

        private int m_animIDBackwardVelocity;

        private int m_animIDNormalizedVerticalVelocity;

        private int m_animIDIsGrounded;

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