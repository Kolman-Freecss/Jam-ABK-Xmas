using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyAttackState
{
    public override EnemyState RunCurrentState()
    {
        return this;
    }
}
