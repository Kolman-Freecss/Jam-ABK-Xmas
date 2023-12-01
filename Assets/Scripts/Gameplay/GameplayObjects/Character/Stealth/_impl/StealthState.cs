#region

#endregion

namespace Gameplay.GameplayObjects.Character.Stealth._impl
{
    public class StealthState : StealthStatus
    {
        public StealthState(CharacterStealthBehaviour characterStealthBehaviour) : base(characterStealthBehaviour)
        {
        }

        public override void Enter()
        {
            m_characterStealthBehaviour.CharacterController.Stealth(true, m_characterStealthBehaviour
                .StealthReductionSpeed);
        }

        public override void Exit()
        {
            m_characterStealthBehaviour.CharacterController.Stealth(false);
        }

        #region Logic Virtual methods

        public override void OnDetected()
        {
            //TODO: Add some animation to show that the character is detected
            m_characterStealthBehaviour.ChangeState(new NormalState(m_characterStealthBehaviour));
        }

        public override void OnStealth()
        {
            m_characterStealthBehaviour.ChangeState(new NormalState(m_characterStealthBehaviour));
        }

        #endregion
    }
}