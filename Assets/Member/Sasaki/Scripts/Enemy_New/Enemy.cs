using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] AnimOperator _animOperator;

    protected override void Setup()
    {
        MonoState
            .AddState(State.Idle, new EnemyStateIdle())
            .AddState(State.Move, new EnemyStateRun())
            .AddState(State.Attack, new EnemyStateAttack());

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
