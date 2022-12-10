using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttack : IBossAttackTask
{
    EnemyAttackCollider _attackCollider;

    void IBossAttackTask.Setup(Transform user, EnemyAttackCollider attackCollider)
    {
        _attackCollider = attackCollider;
        _attackCollider.SetColliderActive(false);
    }

    void IBossAttackTask.Execute()
    {
        _attackCollider.SetColliderActive(true);
    }

    void IBossAttackTask.Initalize()
    {
        _attackCollider.SetColliderActive(false);
    }
}
