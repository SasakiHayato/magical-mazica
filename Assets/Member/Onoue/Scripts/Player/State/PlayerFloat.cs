using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerFloat : MonoStateBase
{
    Player _player;
    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEntry()
    {
        _player.IsJumped = false;
        _player.IsWallJumped = false;
        _player.RigidOperate.SetMoveDirection = Vector2.zero;
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _player.Direction.normalized;
        _player.RigidOperate.SetMoveDirection = new Vector2(dir.x * _player.Speed, _player.RigidOperate.ReadVelocity.y);
    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true)
            && MathF.Abs(_player.RigidOperate.ReadVelocity.x) <= 0)
        {
            return Player.PlayerState.IsStick;
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            return ReturneDefault();
        }
        return Player.PlayerState.Float;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

