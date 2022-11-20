using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerIdle : MonoStateBase
{
    Player _player;
    //Stateが変わる度に呼ばれる
    public override void OnEntry()
    {
        _player.Rigidbody.velocity = Vector3.zero;
    }
    //Update
    public override void OnExecute()
    {

    }
    //条件分岐
    public override Enum OnExit()
    {
        if (_player.IsJumped)
        {
            return Player.PlayerState.Jump;
        }
        if (_player.Direction != Vector2.zero)
        {
            return Player.PlayerState.Run;
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            if (_player.Rigidbody.velocity.y <= 0)
            {
                return Player.PlayerState.Float;
            }
        }
        return Player.PlayerState.Idle;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

