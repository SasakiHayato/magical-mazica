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
        //_player.Rigidbody.velocity = Vector3.zero;
        _player.IsJumped = false;
        _player.IsWallJumped = false;
    }
    //Update
    public override void OnExecute()
    {

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
            if (_player.Rigidbody.velocity.y <= 0)
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

