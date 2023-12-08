using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtBox : MonoBehaviour
{
    [SerializeField] public UnityEvent <float> onHitNotified;
    [SerializeField] public UnityEvent <float, Transform> onHitNotifiedWithOffender;
    [SerializeField] public UnityEvent <float,Hitbox> onHitNotifiedWithBox;
    
    /*
    [SerializeField] public UnityEvent <float,Barrel> onHitNotifiedWithBarrel;
    [SerializeField] public UnityEvent <float,EntityMeleeAttack> onHitNotifiedWithMeleeAttack;
    */

    public virtual void NotifyHit(Hitbox hitbox, float damage)
    {
        onHitNotified.Invoke(damage);
        onHitNotifiedWithOffender.Invoke(damage, hitbox.transform);
        onHitNotifiedWithBox.Invoke(damage, hitbox);
    }

    /*
    public virtual void NotifyHit(Barrel barrel, float damage)
    {
        onHitNotified.Invoke(damage);
        onHitNotifiedWithOffender.Invoke(damage, barrel.transform);
        onHitNotifiedWithBarrel.Invoke(damage, barrel);
    }

    public virtual void NotifyHit(EntityMeleeAttack meleeAttack, float damage)
    {
        onHitNotified.Invoke(damage);
        onHitNotifiedWithOffender.Invoke(damage, meleeAttack.transform);
        onHitNotifiedWithMeleeAttack.Invoke(damage, meleeAttack);
    }
    */
}
