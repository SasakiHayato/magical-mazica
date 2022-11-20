using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerFloat : MonoStateBase
{
    Player _player;
    //State���ς��x�ɌĂ΂��
    public override void OnEntry()
    {
        _player.IsJumped = false;
        _player.IsWallJumped = false;
    }
    //Update
    public override void OnExecute()
    {
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            if (_player.Direction == Vector2.zero)
            {
                _player.Rigidbody.velocity = Vector2.down;
            }
            else
            {
                _player.Rigidbody.velocity = Vector2.zero;
                //if (_player.Direction.x < 0)
                //{
                //    //�G��Ă���ǂ̕����Ɍ������ē��͂���ƕǂɒ���t���悤�ɂ���
                    
                //}
                //else
                //{
                    
                //}
            }
        }
        else
        {
            Vector2 dir = _player.Direction;
            Vector2 velocity = new Vector2(dir.x * _player.Speed, _player.Rigidbody.velocity.y);
            _player.Rigidbody.velocity = velocity;
        }
    }
    //��������
    public override Enum OnExit()
    {
        if (_player.IsWallJumped)
        {
            return Player.PlayerState.WallJump;
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

