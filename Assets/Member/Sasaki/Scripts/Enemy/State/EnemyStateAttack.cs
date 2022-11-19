using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;
using MonoState.Data;

public class EnemyStateAttack : MonoStateBase
{
    EnemyStateData _enemyData;
    EnemyAIData _aiData;

    float _testTimer = 0;

    public override void Setup(MonoStateData data)
    {
        _enemyData = data.GetMonoData<EnemyStateData>(nameof(EnemyStateData));

        _aiData = data.GetMonoData<EnemyAIData>(nameof(EnemyAIData));
        _aiData.AttackData.Attack.Setup(data.StateUser);
    }

    public override void OnEntry()
    {
        _testTimer = 0;
        _aiData.AttackData.Attack.Initalize();
        _aiData.AttackData.Attack.AttackCollider.SetColliderActive(true);
    }

    public override void OnExecute()
    {
        _testTimer += Time.deltaTime;

        _enemyData.MoveDir = _aiData.AttackData.Attack.OnMove();
    }

    public override Enum OnExit()
    {
        if (_testTimer > 1)
        {
            _aiData.AttackData.Attack.AttackCollider.SetColliderActive(false);
            return ReturneDefault();
        }

        return EnemyBase.State.Attack;
    }
}
