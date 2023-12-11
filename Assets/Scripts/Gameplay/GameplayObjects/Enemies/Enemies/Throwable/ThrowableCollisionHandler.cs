using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //if collisioned with enemy, activate the stun
            other.gameObject.GetComponent<EnemyIdleState>().SwitchTimer();
        }
    }
}
