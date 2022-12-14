using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
public class PlayerRun : MonoStateBase
{
    Player _player;
    AnimOperator _anim;
    PlayerStateData _playerStateData;

    //Stateが変わる度に呼ばれる
    public override void OnEntry()
    {
        _anim.OnPlay("Run");
    }
    //Update
    public override void OnExecute()
    {
        _player.FieldTouchOperator.IsTouchLayerPath(out string[] aa);
        
        Vector2 dir = _playerStateData.ReadMoveDirection;
        Vector2 velo = dir.normalized;
        _playerStateData.Rigid.SetMoveDirection = new Vector2(velo.x * _playerStateData.Status.Speed, 0);
    }
    //条件分岐
    public override Enum OnExit()
    {
        if (_playerStateData.ReadMoveDirection == Vector2.zero)
        {
            return Player.PlayerState.Idle;
        }
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            if (_playerStateData.Rigid.ReadVelocity.y <= 0)
            {
                return Player.PlayerState.Float;
            }
        }
        return Player.PlayerState.Run;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
        _playerStateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
    }
}

