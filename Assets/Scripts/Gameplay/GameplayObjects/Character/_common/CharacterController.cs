#region

using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character._common
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UnityEngine.CharacterController))]
    public class CharacterController : AnimableCharacterController
    {
        #region Inspector Variables

        [Header("Character Controller")] [Tooltip("Movement speed of the player")] [SerializeField]
        protected float _speed = 6f;

        [Tooltip("Sprint speed of the player")] [SerializeField]
        protected float _sprintSpeed = 12f;

        [Tooltip("How fast the character turns to face movement direction")] [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Header("Player Grounded")] [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Tooltip("Useful for rough ground")] public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        #endregion

        #region Member Variables

        protected UnityEngine.CharacterController m_controller;

        #endregion
    }
}