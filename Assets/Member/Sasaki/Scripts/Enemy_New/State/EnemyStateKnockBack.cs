using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.State;
using System;
using MonoState.Data;
using BehaviourTree;

public class EnemyStateKnockBack : MonoStateBase
{
    float _timer = 0;

    BehaviourTreeUser _treeUser;

    readonly float _knockTime = 1;

    public override void Setup(MonoStateData data)
    {
        _treeUser = data.GetMonoDataUni<BehaviourTreeUser>(nameof(BehaviourTreeUser));
    }

    public override void OnEntry()
    {
        _timer = 0;
        _treeUser.SetRunRequest(false);
    }

    public override void OnExecute()
    {
        _timer += Time.deltaTime;
    }

    public override Enum OnExit()
    {
        if (_timer > _knockTime)
        {
            _treeUser.SetRunRequest(true);
            return EnemyBase.State.Idle;
        }

        return EnemyBase.State.KnockBack;
    }
}
