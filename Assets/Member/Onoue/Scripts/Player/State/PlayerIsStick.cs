using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerIsStick : MonoStateBase
{
    Player _player;
    float _acceleration;
    int[] _id;
    //Stateが変わる度に呼ばれる
    public override void OnEntry()
    {
        _player.RigidOperate.SetMoveDirection = Vector2.zero;
        _player.RigidOperate.SetGravityRate(0.1f);
        _acceleration = _player.RigidOperate.MaxAcceleration;
        _player.RigidOperate.MaxAcceleration = 0;
        _player.FieldTouchOperator.IsTouchLayerID(out _id);
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _player.Direction;
        Vector2 velocity = new Vector2(dir.x * _player.Speed, _player.RigidOperate.ReadVelocity.y);
        //触れている壁側に入力しているとバグが起きる
        if (_player.Direction == Vector2.zero)
        {
            _player.RigidOperate.SetMoveDirection = Vector2.down;
        }
        if (_id[0] == 2)
        {
            if (_player.Direction.x > 0)
            {
                _player.RigidOperate.SetMoveDirection = Vector2.zero;
            }
            else if (_player.Direction.x < 0)
            {
                _player.RigidOperate.SetMoveDirection = velocity;
            }
        }
        else if (_id[0] == 3)
        {
            if (_player.Direction.x < 0)
            {
                _player.Direction = Vector2.zero;
            }
            else if (_player.Direction.x > 0)
            {
                _player.RigidOperate.SetMoveDirection = velocity;
            }
        }
    }
    //条件分岐
    public override Enum OnExit()
    {
        if (_player.IsWallJumped)
        {
            _player.RigidOperate.SetGravityRate(1);
            _player.RigidOperate.MaxAcceleration = _acceleration;
            return Player.PlayerState.WallJump;
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            if (_player.RigidOperate.ReadVelocity.y <= 0)
            {
                _player.RigidOperate.SetGravityRate(1);
                _player.RigidOperate.MaxAcceleration = _acceleration;
                return Player.PlayerState.Float;
            }
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            _player.RigidOperate.SetGravityRate(1);
            _player.RigidOperate.MaxAcceleration = _acceleration;
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

