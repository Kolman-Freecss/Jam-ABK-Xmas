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
            m_characterStealthBehaviour.CharacterController.StopMovement();
        }

        public override void Exit()
        {
            m_characterStealthBehaviour.CharacterController.ResetSpeed();
        }

        #region Logic Virtual methods

        public override void OnDetected()
        {
            m_characterStealthBehaviour.ChangeState(new StealthState(m_characterStealthBehaviour));
        }

        #endregion
    }
}