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

        #endregion
    }
}