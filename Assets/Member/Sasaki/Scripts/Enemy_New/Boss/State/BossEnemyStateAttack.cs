using System;
using System.Collections.Generic;
using MonoState.State;
using MonoState.Data;

public class BossEnemyStateAttack : MonoStateBase
{
    Boss_NewData _data;
    AnimOperator _anim;

    public override void Setup(MonoStateData data)
    {
        _data = data.GetMonoData<Boss_NewData>(nameof(Boss_NewData));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
    }

    public override void OnEntry()
    {
        _data.ReadCollider.SetColliderActive(false);

        AnimOperator.AnimEvent anim = new AnimOperator.AnimEvent
        {
            Frame = _data.ReadAttackFrame.Item1,
            Event = () => _data.ReadCollider.SetColliderActive(true),
        };

        AnimOperator.AnimEvent anim2 = new AnimOperator.AnimEvent
        {
            Frame = _data.ReadAttackFrame.Item2,
            Event = () => _data.ReadCollider.SetColliderActive(false),
        };

        List<AnimOperator.AnimEvent> list = new List<AnimOperator.AnimEvent>();
        list.Add(anim);
        list.Add(anim2);

        _anim?.OnPlay("Mazic", list);
    }

    public override void OnExecute()
    {
        
    }

    public override Enum OnExit()
    {
        if (!_data.ReadOnAttack && _anim.EndCurrentAnim)
        {
            return EnemyBase.State.Move;
        }

        return EnemyBase.State.Attack;
    }
}
