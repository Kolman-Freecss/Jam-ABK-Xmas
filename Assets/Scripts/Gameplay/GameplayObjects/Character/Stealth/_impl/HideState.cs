#region

#endregion

namespace Gameplay.GameplayObjects.Character.Stealth._impl
{
    public class HideState : StealthStatus
    {
        public HideState(CharacterStealthBehaviour characterStealthBehaviour) : base(characterStealthBehaviour)
        {
        }

        public override void Enter()
        {
            m_characterStealthBehaviour.CharacterController.Hide(true);
        }

        public override void Exit()
        {
            m_characterStealthBehaviour.CharacterController.Hide(false);
        }

        #region Logic Virtual methods

        public override void OnDetected()
        {
            //TODO: Add some animation to show that the character is detected
            m_characterStealthBehaviour.ChangeState(new NormalState(m_characterStealthBehaviour));
        }

        public override void OnHide()
        {
            m_characterStealthBehaviour.ChangeState(new NormalState(m_characterStealthBehaviour));
        }

        #endregion
    }
}