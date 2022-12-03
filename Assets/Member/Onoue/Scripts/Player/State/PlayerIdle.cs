using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerIdle : MonoStateBase
{
    Player _player;
    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEntry()
    {
        _player.RigidOperate.SetMoveDirection = Vector2.zero;
        _player.IsJumped = false;
        _player.IsWallJumped = false;
    }
    //Update
    public override void OnExecute()
    {
        _player.FieldTouchOperator.IsTouchLayerPath(out string[] aa);
    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_player.IsJumped)
        {
            return Player.PlayerState.Jump;
        }
        if (_player.Direction.x != 0)
        {
            return Player.PlayerState.Run;
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            if (_player.RigidOperate.ReadVelocity.y <= 0)
            {
                return Player.PlayerState.Float;
            }
        }
        return Player.PlayerState.Idle;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

