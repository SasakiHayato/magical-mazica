using System;
using MonoState.State;
using MonoState.Data;

public class EnemyStateMove : MonoStateBase
{
    EnemyStateData _enemyData;
    EnemyAIData _aiData;

    public override void Setup(MonoStateData data)
    {
        _enemyData = data.GetMonoData<EnemyStateData>(nameof(EnemyStateData));

        _aiData = data.GetMonoData<EnemyAIData>(nameof(EnemyAIData));
        _aiData.MoveData.Move.Setup(data.StateUser);
    }

    public override void OnEntry()
    {
        _aiData.MoveData.Move.Initalize();
    }

    public override void OnExecute()
    {
        _enemyData.MoveDir = _aiData.MoveData.Move.OnMove() * _aiData.MoveData.Move.AttributeSpeed;
    }

    public override Enum OnExit()
    {
        if (_aiData.AttackData.AttributeDistance > _enemyData.PlayerDistance)
        {
            return EnemyBase.State.Attack;
        }

        if (_aiData.MoveData.AttributeDistance < _enemyData.PlayerDistance)
        {
            return EnemyBase.State.Idle;
        }

        return EnemyBase.State.Move;
    }
}
