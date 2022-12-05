using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerIdle : MonoStateBase
{
    Player _player;
    PlayerStateData _playerStateData;

    //Stateが変わる度に呼ばれる
    public override void OnEntry()
    {
        _playerStateData.Rigid.SetMoveDirection = Vector2.zero;
        _playerStateData.Jump.InitalizeJumpCount();
    }
    //Update
    public override void OnExecute()
    {
        _player.FieldTouchOperator.IsTouchLayerPath(out string[] aa);
    }
    //条件分岐
    public override Enum OnExit()
    {
        if (_playerStateData.ReadMoveDirection.x != 0)
        {
            return Player.PlayerState.Run;
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            if (_playerStateData.Rigid.ReadVelocity.y <= 0)
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
        _playerStateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
    }
}

