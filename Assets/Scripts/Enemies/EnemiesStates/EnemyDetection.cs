using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle;

    [SerializeField] LayerMask isPlayer, isObstacle;
    Transform player;

    //check neither player is being detected, called in EnemyIdleState
    public bool OnPlayerDetection()
    {
        Vector3 playerTarget = (player.position - transform.position).normalized;

        if(Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.position);
            if(distanceToTarget <= viewRadius)
            {
                if (Physics2D.Raycast(transform.position, playerTarget, distanceToTarget, isObstacle) == false)
                {
                    Debug.Log("visto");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
