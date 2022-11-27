using System;
using MonoState.Data;
using MonoState.State;
using EnemyAISystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyStateAttack : MonoStateBase
{
    Transform _user;
    EnemyStateData _enemyData;
    EnemyAIData _aiData;

    bool _isAttack;
    bool _onMove;

    public override void Setup(MonoStateData data)
    {
        _enemyData = data.GetMonoData<EnemyStateData>(nameof(EnemyStateData));

        _user = data.StateUser;

        _aiData = data.GetMonoData<EnemyAIData>(nameof(EnemyAIData));
        _aiData.AttackData.Attack?.Setup(data.StateUser);
        _aiData.AttackData.AttackEvent?.Setup(data.StateUser);
    }

    public override void OnEntry()
    {
        _isAttack = false;
        _onMove = false;
        _aiData.AttackData.Attack.Initalize();
        _aiData.AttackData.AttackEvent?.EnableEvent();
        
        AwaitAttack().Forget();
    }

    async UniTask AwaitAttack()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_aiData.AttackData.AttackWaitTime));
        OnColliderActive().Forget();
        OnAttck().Forget();
        _onMove = true;
    }

    public override void OnExecute()
    {
        if (!_onMove) return;
        
        _aiData.AttackData.AttackEvent?.ExecuteEvent();
        _enemyData.MoveDir = _aiData.AttackData.Attack.OnMove() * _aiData.AttackData.Attack.AttributeSpeed;
    }

    async UniTask OnColliderActive()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_aiData.AttackData.Attack.ColliderIsActiveTime));
        _aiData.AttackData.Attack.AttackCollider?.SetColliderActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(_aiData.AttackData.Attack.ColliderActiveTime));
        _aiData.AttackData.Attack.AttackCollider?.SetColliderActive(false);
    }

    async UniTask OnAttck()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_aiData.AttackData.Attack.IsAttackTime));
        
        _isAttack = true;
    }

    public override Enum OnExit()
    {
        if (_isAttack)
        {
            _aiData.AttackData.Attack.AttackCollider?.SetColliderActive(false);
            _aiData.AttackData.AttackEvent?.EndEvent();
            return ReturneDefault();
        }

        return EnemyBase.State.Attack;
    }
}
