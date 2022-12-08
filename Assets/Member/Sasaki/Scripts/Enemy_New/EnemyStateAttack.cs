using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;
using MonoState.Data;

public class EnemyStateAttack : MonoStateBase
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
        _stateData.AttackCollider.SetColliderActive(false);

        AnimOperator.AnimEvent anim = new AnimOperator.AnimEvent
        {
            Frame = _stateData.AttackAciveFrame.Item1,
            Event = () => _stateData.AttackCollider.SetColliderActive(true),
        };

        AnimOperator.AnimEvent anim2 = new AnimOperator.AnimEvent
        {
            Frame = _stateData.AttackAciveFrame.Item2,
            Event = () => _stateData.AttackCollider.SetColliderActive(false),
        };

        List<AnimOperator.AnimEvent> list = new List<AnimOperator.AnimEvent>();
        list.Add(anim);
        list.Add(anim2);

        _anim.OnPlay("Attack", list);
    }

    public override void OnExecute()
    {
        
    }

    public override Enum OnExit()
    {
        if (_anim.EndCurrentAnim)
        {
            _stateData.IBehaviourDatable.OnAttack = false;
            return ReturneState();
        }

        return EnemyBase.State.Attack;
    }
}
