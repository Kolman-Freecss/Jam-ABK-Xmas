#region

using Gameplay.GameplayObjects.Character.Stealth._impl;
using UnityEngine;
using UnityEngine.Events;
using CharacterController = Gameplay.GameplayObjects.Character._common.CharacterController;

#endregion

namespace Gameplay.GameplayObjects.Character
{
    /// <summary>
    /// You could override this class to add stealth behaviour to your character. (Enemy AI, Player, etc.)
    /// </summary>
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public abstract class CharacterStealthBehaviour : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Events")] public UnityEvent<StealthStatus> onStealthStatusChanged;

        [Header("Stealth Settings")] [SerializeField]
        protected float stealthReductionSpeed = 2f;

        #endregion

        #region Member Variables

        protected StealthStatus m_currentStealthStatus;
        protected CharacterController m_characterController;

        #endregion

        #region Logic

        private void Start()
        {
            m_currentStealthStatus = new NormalState(this);
            m_characterController = GetComponent<CharacterController>();
        }

        #endregion

        #region Virtual methods

        public virtual void ChangeState(StealthStatus newState)
        {
            m_currentStealthStatus.Exit();
            m_currentStealthStatus = newState;
            m_currentStealthStatus.Enter();
            onStealthStatusChanged?.Invoke(newState);
        }

        public virtual void OnDetect()
        {
            m_currentStealthStatus.OnDetected();
        }

        protected virtual void OnAppear()
        {
            m_currentStealthStatus.OnAppear();
        }

        protected virtual void OnHide()
        {
            m_currentStealthStatus.OnStealth();
        }

        protected virtual void OnStealth()
        {
            m_currentStealthStatus.OnStealth();
        }

        #endregion

        #region Getter & Setter

        public StealthStatus CurrentStealthStatus => m_currentStealthStatus;

        public CharacterController CharacterController => m_characterController;

        public float StealthReductionSpeed => stealthReductionSpeed;

        #endregion
    }
}