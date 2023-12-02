#region

using Gameplay.GameplayObjects.Character.Player;
using UnityEngine;

#endregion

public class BossWeapon : MonoBehaviour
{
    [SerializeField] int attackDamage = 20;

    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D coll = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (coll != null)
        {
            coll.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}