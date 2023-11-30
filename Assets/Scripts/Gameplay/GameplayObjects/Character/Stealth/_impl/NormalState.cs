namespace Gameplay.GameplayObjects.Character.Stealth._impl
{
    public class NormalState : StealthStatus
    {
        public NormalState(CharacterStealthBehaviour characterStealthBehaviour) : base(characterStealthBehaviour)
        {
        }

        public override void Enter()
        {
            m_characterStealthBehaviour.CharacterController.ResetSpeed();
        }

        public override void Exit()
        {
        }

        #region Logic Virtual methods

        public override void OnDetected()
        {
            //Alert the character (enemy or player)
        }

        public override void OnStealth()
        {
            m_characterStealthBehaviour.ChangeState(new StealthState(m_characterStealthBehaviour));
        }

        public override void OnHide()
        {
            m_characterStealthBehaviour.ChangeState(new HideState(m_characterStealthBehaviour));
        }

        #endregion
    }
}