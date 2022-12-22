using System;
using MonoState.State;
using MonoState.Data;
using BehaviourTree;

public class EnemyStateKnockBack : MonoStateBase
{
    BehaviourTreeUser _treeUser;
    EnemyStateData _stateData;
    AnimOperator _anim;

    public override void Setup(MonoStateData data)
    {
        _treeUser = data.GetMonoDataUni<BehaviourTreeUser>(nameof(BehaviourTreeUser));
        _stateData = data.GetMonoData<EnemyStateData>(nameof(EnemyStateData));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEntry()
    {
        _treeUser.SetRunRequest(false);
        _stateData.Rigid.UseInertia = true;
        _anim.OnPlay("Idle");
    }

    public override void OnExecute()
    {
        
    }

    public override Enum OnExit()
    {
        if (!_stateData.Rigid.IsMoveMock)
        {
            _treeUser.SetRunRequest(true);
            _stateData.Rigid.UseInertia = false;

            return EnemyBase.State.Idle;
        }

        return EnemyBase.State.KnockBack;
    }
}
