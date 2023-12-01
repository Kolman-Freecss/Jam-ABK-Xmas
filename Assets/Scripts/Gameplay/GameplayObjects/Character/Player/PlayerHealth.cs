#region

using UnityEngine;

#endregion

namespace Gameplay.GameplayObjects.Character.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] int life;

        public void TakeDamage(int damage)
        {
            life -= damage;
        }
    }
}