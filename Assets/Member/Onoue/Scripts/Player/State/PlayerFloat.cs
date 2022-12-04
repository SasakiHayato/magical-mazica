using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerFloat : MonoStateBase
{
    Player _player;
    PlayerStateData _playerStateData;

    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEntry()
    {
        _playerStateData.Rigid.SetMoveDirection = Vector2.zero;
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _playerStateData.ReadMoveDirection;
        _playerStateData.Rigid.SetMoveDirection = new Vector2(dir.x * _playerStateData.Status.Speed, _playerStateData.Rigid.ReadVelocity.y);
    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true)
            && MathF.Abs(_playerStateData.Rigid.ReadVelocity.x) <= 0)
        {
            return Player.PlayerState.IsStick;
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            return ReturneState();
        }
        return Player.PlayerState.Float;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
        _playerStateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
    }
}

