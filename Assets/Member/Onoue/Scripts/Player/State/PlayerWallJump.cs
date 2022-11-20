using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerWallJump : MonoStateBase
{
    Player _player;
    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEntry()
    {
        _player.Rigidbody.velocity = Vector2.zero;
        if (_player.Direction.x != 0)
        {
            if (_player.Direction.x < 0)
            {
                //¶“ü—Í
                _player.Rigidbody.AddForce(Vector2.one * _player.JumpPower, ForceMode2D.Impulse);
            }
            else
            {
                //‰E“ü—Í
                _player.Rigidbody.AddForce(new Vector2(-1, 1) * _player.JumpPower, ForceMode2D.Impulse);
            }
        }
        _player.IsWallJumped = false;
    }
    //Update
    public override void OnExecute()
    {

    }
    //ðŒ•ªŠò
    public override Enum OnExit()
    {
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            if (_player.Rigidbody.velocity.y <= 0)
            {
                return Player.PlayerState.Float;
            }
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground))
        {
            return ReturneDefault();
        }
        return Player.PlayerState.WallJump;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

