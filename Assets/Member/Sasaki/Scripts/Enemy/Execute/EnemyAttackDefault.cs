using EnemyAISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDefault : IEnemyAttack
{
    EnemyAttackCollider _enemyAttackCollider;

    float IEnemyAttack.AttributeSpeed => 1;
    float IEnemyAttack.IsAttackTime => 0;
    EnemyAttackCollider IEnemyAttack.AttackCollider => _enemyAttackCollider;
    
    public void Setup(Transform user)
    {
        _enemyAttackCollider = user.GetComponentInChildren<EnemyAttackCollider>();
    }

    public Vector2 OnMove()
    {
        return Vector2.zero;
    }

    public void Initalize()
    {
        
    }
}
