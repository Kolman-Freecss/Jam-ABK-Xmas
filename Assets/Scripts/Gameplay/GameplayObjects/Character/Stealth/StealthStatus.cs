namespace Gameplay.GameplayObjects.Character
{
    /// <summary>
    /// This class is responsible for managing the stealth status of a character.
    /// </summary>
    public abstract class StealthStatus
    {
        #region Member variables

        protected CharacterStealthBehaviour m_characterStealthBehaviour;

        #endregion

        protected StealthStatus(CharacterStealthBehaviour characterStealthBehaviour)
        {
            m_characterStealthBehaviour = characterStealthBehaviour;
        }

        #region Abstract Methods

        public abstract void Enter();

        public abstract void Exit();

        #endregion

        #region Virtual methods

        public virtual void OnStealth()
        {
        }

        public virtual void OnAppear()
        {
        }

        public virtual void OnHide()
        {
        }

        /// <summary>
        /// When the character is detected by an enemy.
        /// </summary>
        public virtual void OnDetected()
        {
        }

        #endregion
    }
}