using System;
using MonoState.State;
using MonoState.Data;
using EnemyAISystem;

public class EnemyStateIdle : MonoStateBase
{
    EnemyStateData _enemyData;
    EnemyAIData _aiData;

    public override void Setup(MonoStateData data)
    {
        _enemyData = data.GetMonoData<EnemyStateData>(nameof(EnemyStateData));

        _aiData = data.GetMonoData<EnemyAIData>(nameof(EnemyAIData));
        _aiData.IdleData.Idle.Setup(data.StateUser);
    }

    public override void OnEntry()
    {
        _aiData.IdleData.Idle.Initalize();
    }

    public override void OnExecute()
    {
        _enemyData.MoveDir = _aiData.IdleData.Idle.OnMove();
    }

    public override Enum OnExit()
    {
        if (_aiData.AttackData.AttributeDistance > _enemyData.PlayerDistance)
        {
            return EnemyBase.State.Attack;
        }

        if (_aiData.MoveData.AttributeDistance > _enemyData.PlayerDistance)
        {
            return EnemyBase.State.Move;
        }

        return EnemyBase.State.Idle;
    }
}
