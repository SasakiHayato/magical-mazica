using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerIsStick : MonoStateBase
{
    Player _player;
    int[] _id;
    //StateÇ™ïœÇÌÇÈìxÇ…åƒÇŒÇÍÇÈ
    public override void OnEntry()
    {
        Debug.Log("In WallState");

        _player.Rigidbody.velocity = Vector3.zero;
        _player.FieldTouchOperator.IsTouchLayerID(out _id);
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _player.Direction;
        Vector2 velocity = new Vector2(dir.x * _player.Speed, _player.Rigidbody.velocity.y);
        if (_player.Direction == Vector2.zero)
        {
            _player.Rigidbody.velocity = Vector2.down;
        }
        if (_id[0] == 2 && _player.Direction.x < 0)
        {
            _player.Rigidbody.velocity = velocity;
        }
        else if (_id[0] == 3 && _player.Direction.x > 0)
        {
            _player.Rigidbody.velocity = velocity;
        }
    }
    //èåèï™äÚ
    public override Enum OnExit()
    {
        if (_player.IsWallJumped)
        {
            return Player.PlayerState.WallJump;
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            if (_player.Rigidbody.velocity.y < 0)
            {
                return Player.PlayerState.Float;
            }
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            return ReturneDefault();
        }
        return Player.PlayerState.IsStick;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

