using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerWallJump : MonoStateBase
{
    Player _player;
    //StateÇ™ïœÇÌÇÈìxÇ…åƒÇŒÇÍÇÈ
    public override void OnEntry()
    {
        //_player.Rigidbody.velocity = Vector2.zero;
        _player.FieldTouchOperator.IsTouchLayerID(out int[] id);
        if (id[0] == 2)
        {
            _player.Rigidbody.AddForce(new Vector2(-1, 1) * _player.WallJumpPower, ForceMode2D.Impulse);
        }
        else if (id[0] == 3)
        {
            _player.Rigidbody.AddForce(Vector2.one * _player.WallJumpPower, ForceMode2D.Impulse);
        }
    }
    //Update
    public override void OnExecute()
    {

    }
    //èåèï™äÚ
    public override Enum OnExit()
    {
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true)
            && !_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            if (_player.Rigidbody.velocity.y <= 0)
            {
                _player.IsWallJumped = false;
                return Player.PlayerState.Float;
            }
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            return ReturneDefault();
        }
        //Ç±Ç±Ç™å¥àˆ
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true) 
            && MathF.Abs(_player.Rigidbody.velocity.x) <= 0)
        {
            _player.IsWallJumped = false;
            return Player.PlayerState.IsStick;
        }
        return Player.PlayerState.WallJump;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

