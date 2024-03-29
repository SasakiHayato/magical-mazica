using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerJump : MonoStateBase
{
    Player _player;
    AnimOperator _anim;
    PlayerStateData _playerStateData;
    float _speed;
    //Stateが変わる度に呼ばれる
    public override void OnEntry()
    {
        _playerStateData.Rigid.SetImpulse(_playerStateData.Jump.Power, RigidMasterData.ImpulseDirectionType.Vertical,true);
        _playerStateData.Jump.CallbackJumpCount();
        _speed = _playerStateData.Rigid.ReadVelocity.x;
        
        _anim.OnPlay("Jump");
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _playerStateData.ReadMoveDirection;
        _playerStateData.Rigid.SetMoveDirection = new Vector2(dir.x * _playerStateData.Status.Speed, _playerStateData.Rigid.ReadVelocity.y);
        if (_speed == 0)
        {
            
        }
    }
    //条件分岐
    public override Enum OnExit()
    {
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true)
            && !_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            if (_playerStateData.Rigid.ReadVelocity.y <= 0)
            {
                return Player.PlayerState.Float;
            }
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            return Player.PlayerState.IsStick;
        }

        return Player.PlayerState.Jump;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
        _playerStateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
    }
}
