using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerJump : MonoStateBase
{
    Player _player;
    //State‚ª•Ï‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    public override void OnEntry()
    {
        _player.Rigidbody.AddForce(Vector2.up * _player.JumpPower, ForceMode2D.Impulse);
    }
    //Update
    public override void OnExecute()
    {

    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground))
        {
            return ReturneDefault();
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground))
        {
            if (_player.Rigidbody.velocity.y < 0)
            {
                return Player.PlayerState.Float;
            }
        }
        if (_player.IsWallJumped)
        {
            return Player.PlayerState.WallJump;
        }
        return Player.PlayerState.Jump;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}
