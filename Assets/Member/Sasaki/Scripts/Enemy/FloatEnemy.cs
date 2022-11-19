using UnityEngine;
using EnemyAISystem;

public class FloatEnemy : EnemyBase
{
    [SerializeField] EnemyAIData _enemyAIData;

    protected override void Setup()
    {
        MonoState
            .AddState(State.Idle, new EnemyStateIdle())
            .AddState(State.Move, new EnemyStateMove())
            .AddState(State.Attack, new EnemyStateAttack());

        MonoState.AddMonoData(_enemyAIData);

        MonoState.IsRun = true;
    }

    protected override void Execute()
    {
        Vector2 move = EnemyStateData.MoveDir * Speed;
        RB.velocity = move;
    }

    protected override bool IsDamage(int damage)
    {
        return true;
    }
}
