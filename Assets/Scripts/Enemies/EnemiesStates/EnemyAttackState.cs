using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public override EnemyState RunCurrentState()
    {
        //catch player
        Debug.Log("attack");
        return this;
    }
}
