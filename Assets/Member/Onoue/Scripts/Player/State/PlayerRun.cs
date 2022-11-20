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
        //Debug.Log("Entry PlayerRun");
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _player.Direction;
        Vector2 velocity = new Vector2(dir.x * _player.Speed, _player.Rigidbody.velocity.y);
        _player.Rigidbody.velocity = velocity;
    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_player.IsJumped)
        {
            Debug.Log("Run Jump");
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
            if (_player.Rigidbody.velocity.y <= 0)
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

