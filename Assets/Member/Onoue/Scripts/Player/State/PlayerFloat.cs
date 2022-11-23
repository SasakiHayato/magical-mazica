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
        
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _player.Direction;
        Vector2 velo = dir.normalized;
        _player.Rigidbody.velocity = new Vector2(velo.x * _player.Speed, _player.Rigidbody.velocity.y);
    }
    //ğŒ•ªŠò
    public override Enum OnExit()
    {
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true)
            && MathF.Abs(_player.Rigidbody.velocity.x) <= 0)
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

