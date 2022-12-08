using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;
using MonoState.Data;

public class EnemyStateRun : MonoStateBase
{
    EnemyStateData _stateData;
    AnimOperator _anim;

    public override void Setup(MonoStateData data)
    {
        _stateData = data.GetMonoData<EnemyStateData>(nameof(EnemyStateData));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEntry()
    {
        _anim?.OnPlay("Run");
    }

    public override void OnExecute()
    {
        
    }

    public override Enum OnExit()
    {
        if (_stateData.IBehaviourDatable.OnAttack)
        {
            return EnemyBase.State.Attack;
        }

        if (_stateData.MoveDirection == Vector2.zero)
        {
            return EnemyBase.State.Idle;
        }

        return EnemyBase.State.Move;
    }
}
