using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
using MonoState.Opration;

public class PlayerIsStick : MonoStateBase, IStateExitEventable
{
    Player _player;
    AnimOperator _anim;
    PlayerStateData _playerStateData;
    float _acceleration;
    int[] _id;

    void IStateExitEventable.ExitEvent()
    {
        _playerStateData.Rigid.MaxAcceleration = _acceleration;
    }

    //Stateが変わる度に呼ばれる
    public override void OnEntry()
    {
        _playerStateData.Rigid.SetMoveDirection = Vector2.zero;
        _acceleration = _playerStateData.Rigid.MaxAcceleration;
        _playerStateData.Rigid.MaxAcceleration = 0;
        _player.FieldTouchOperator.IsTouchLayerID(out _id);
        if (_id[0] == 2)
        {
            _player.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_id[0] == 3)
        {
            _player.transform.localScale = new Vector3(1, 1, 1);
        }
        _playerStateData.Rigid.ResetImpalse();
        _anim.OnPlay("IsStick");
    }
    //Update
    public override void OnExecute()
    {
        Vector2 dir = _playerStateData.ReadMoveDirection;
        Vector2 velocity = new Vector2(dir.x * _playerStateData.Status.Speed, _playerStateData.Rigid.ReadVelocity.y);
        //触れている壁側に入力しているとバグが起きる
        if (_playerStateData.ReadMoveDirection == Vector2.zero)
        {
            _playerStateData.Rigid.SetMoveDirection = Vector2.down;
        }
        if (_id[0] == 2)
        {
            if (_playerStateData.ReadMoveDirection.x > 0)
            {
                _playerStateData.Rigid.SetMoveDirection = Vector2.zero;
            }
            else if (_playerStateData.ReadMoveDirection.x < 0)
            {
                _playerStateData.Rigid.SetMoveDirection = velocity;
            }
        }
        else if (_id[0] == 3)
        {
            if (_playerStateData.ReadMoveDirection.x < 0)
            {
                _playerStateData.SetMoveDirection = Vector2.zero;
            }
            else if (_playerStateData.ReadMoveDirection.x > 0)
            {
                _playerStateData.Rigid.SetMoveDirection = velocity;
            }
        }
    }
    //条件分岐
    public override Enum OnExit()
    {
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            if (_playerStateData.Rigid.ReadVelocity.y <= 0)
            {
                return Player.PlayerState.Float;
            }
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            
            return ReturneState();
        }
        return Player.PlayerState.IsStick;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
        _playerStateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
    }
}

