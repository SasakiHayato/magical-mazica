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

    public override void Setup(MonoStateData data)
    {
        _enemyData = data.GetMonoData<EnemyStateData>(nameof(EnemyStateData));

        _aiData = data.GetMonoData<EnemyAIData>(nameof(EnemyAIData));
        _aiData.AttackData.Attack.Setup(data.StateUser);
    }

    public override void OnEntry()
    {
        _aiData.AttackData.Attack.Initalize();
    }

    public override void OnExecute()
    {
        _enemyData.MoveDir = _aiData.AttackData.Attack.OnMove();
    }

    public override Enum OnExit()
    {
        return EnemyBase.State.Idle;
    }
}
