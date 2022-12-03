using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerRun : MonoStateBase
{
    Player _player;

    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEntry()
    {
    }
    //Update
    public override void OnExecute()
    {
        _player.FieldTouchOperator.IsTouchLayerPath(out string[] aa);
        
        Vector2 dir = _player.Direction;
        Vector2 velo = dir.normalized;
        _player.RigidOperate.SetMoveDirection = new Vector2(velo.x * _player.Speed, 0);
    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_player.IsJumped)
        {
            return Player.PlayerState.Jump;
        }
        if (_player.IsWallJumped)
        {
            return Player.PlayerState.WallJump;
        }
        if (_player.Direction == Vector2.zero)
        {
            return Player.PlayerState.Idle;
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            if (_player.RigidOperate.ReadVelocity.y <= 0)
            {
                return Player.PlayerState.Float;
            }
        }
        return Player.PlayerState.Run;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

