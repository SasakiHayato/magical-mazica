using MonoState.State;
using System;
using MonoState.Data;

public class BossEnemyStateMove : MonoStateBase
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
        _anim.OnPlay("Run");
    }

    public override void OnExecute()
    {
        
    }

    public override Enum OnExit()
    {
        if (_data.ReadOnAttack)
        {
            return EnemyBase.State.Attack;
        }
        
        return EnemyBase.State.Move;
    }
}
