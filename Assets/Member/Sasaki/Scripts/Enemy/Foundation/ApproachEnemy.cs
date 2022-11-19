using UnityEngine;

public class ApproachEnemy : EnemyBase
{
    [SerializeField] EnemyAIData _enemyAIData;
    FieldTouchOperator _fieldTouchOperator;

    protected override void Setup()
    {
        _fieldTouchOperator = GetComponentInChildren<FieldTouchOperator>();

        MonoState
            .AddState(State.Idle, new EnemyStateIdle())
            .AddState(State.Move, new EnemyStateMove())
            .AddState(State.Attack, new EnemyStateAttack());

        MonoState.AddMonoData(_enemyAIData);

        MonoState.IsRun = true;
    }

    protected override void Execute()
    {
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall)
            || !_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground))
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        Vector2 move = EnemyStateData.MoveDir * Speed;
        move.y = Physics2D.gravity.y;
        RB.velocity = move;
    }

    protected override bool IsDamage(int damage)
    {
        return true;
    }
}
