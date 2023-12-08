using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    EnemyCatchRange enemyCatchRange;
    EnemyPursueTargetState enemyPursueTargetState;
    EnemyCombatStanceState enemyCombatStanceState;
    EnemyManager enemyManager;

    EnemyAttackAction[] attackActions;
    EnemyAttackAction currentAttack;

    private void Start() 
    {
        enemyCatchRange = GetComponent<EnemyCatchRange>();
        enemyManager = GetComponent<EnemyManager>();
        enemyPursueTargetState = GetComponent<EnemyPursueTargetState>();
        enemyCombatStanceState = GetComponent<EnemyCombatStanceState>();
    }

    public override EnemyState RunCurrentState()
    {
        Vector3 targetDirection = target.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        distanceFromTarget = Vector3.Distance(transform.position, target.position);
        //attack
        if (currentAttack != null)
        {
            if (distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
            {
                return this;
            }
            else if (distanceFromTarget < currentAttack.maximumAttackAngle)
            {
                if (enemyManager.viewableAngle <= currentAttack.maximumAttackAngle && 
                    enemyManager.viewableAngle >= currentAttack.minimumAttackAngle)
                {
                    if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                        enemyManager.isPerformingAction = true;
                        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                        currentAttack = null;
                        //animation
                        return enemyCombatStanceState;
                    }
                }
            }
        }
        else
        {
            GetNewAttack();
        }
        return enemyCombatStanceState;
    }

    void GetNewAttack()
    {
        int maxScore = 0;
        Vector3 targetDirection = target.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        distanceFromTarget = Vector3.Distance(transform.position, target.position);

        for (int i = 0; i < attackActions.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = attackActions[i];
            if (distanceFromTarget <= enemyAttackAction.maximumDistanteNeededToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && 
                    viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < attackActions.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = attackActions[i];
             if (distanceFromTarget <= enemyAttackAction.maximumDistanteNeededToAttack
                && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && 
                    viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (currentAttack != null)
                        return;
                    
                    temporaryScore += enemyAttackAction.attackScore;

                    if (temporaryScore > randomValue)
                        currentAttack = enemyAttackAction;
                }
            }
        }
    }
}
