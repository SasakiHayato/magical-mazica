using MonoState.State;
using System;
using MonoState.Data;

public class PlayerKnockBack : MonoStateBase
{
    AnimOperator _anim;
    PlayerStateData _stateData;

    public override void Setup(MonoStateData data)
    {
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
        _stateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
    }

    public override void OnEntry()
    {
        _anim.OnPlay("Damage");
        _stateData.Rigid.UseInertia = true;
    }

    public override void OnExecute()
    {
        
    }

    public override Enum OnExit()
    {
        if (!_stateData.Rigid.IsMoveMock)
        {
            _stateData.Rigid.UseInertia = false;
            return ReturneState();
        }

        return Player.PlayerState.KnockBack;
    }
}
