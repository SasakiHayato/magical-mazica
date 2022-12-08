using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] int _attackIsActiveFrame;
    [SerializeField] int _attackEndActiveFrame;
    [SerializeField] AnimOperator _animOperator;

    protected override void Setup()
    {
        MonoState
            .AddState(State.Idle, new EnemyStateIdle())
            .AddState(State.Move, new EnemyStateRun())
            .AddState(State.Attack, new EnemyStateAttack());

        EnemyStateData.AttackAciveFrame = (_attackIsActiveFrame, _attackEndActiveFrame);
        MonoState.AddMonoData(_animOperator);

        MonoState.IsRun = true;
    }

    protected override void Execute()
    {
        Rigid.SetMoveDirection = MoveDirection * Speed;
    }

    protected override bool IsDamage(int damage)
    {
        return true;
    }
}
