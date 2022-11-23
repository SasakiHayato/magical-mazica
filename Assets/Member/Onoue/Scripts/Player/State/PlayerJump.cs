using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerJump : MonoStateBase
{
    Player _player;
    //State���ς��x�ɌĂ΂��
    public override void OnEntry()
    {
        _player.Rigidbody.AddForce(Vector2.up * _player.JumpPower, ForceMode2D.Impulse);
    }
    //Update
    public override void OnExecute()
    {

    }
    //��������
    public override Enum OnExit()
    {
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true)
            && !_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            if (_player.Rigidbody.velocity.y <= 0)
            {
                _player.IsJumped = false;
                return Player.PlayerState.Float;
            }
        }
        if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            _player.IsJumped = false;
            return Player.PlayerState.IsStick;
        }
        return Player.PlayerState.Jump;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}
