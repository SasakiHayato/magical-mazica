using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;
using MonoState.Opration;
public class PlayerDodge : MonoStateBase, IStateExitEventable
{
    Player _player;
    AnimOperator _anim;
    PlayerStateData _playerStateData;
    float _dir,_timer;
    bool _isEnd;
    void IStateExitEventable.ExitEvent()
    {
        _player.gameObject.layer = LayerMask.NameToLayer("Player");
        _player.IsHit = false;
        _isEnd = false;
        _timer = 0;
    }
    public override void OnEntry()
    {
        _dir = _player.transform.localScale.x;
        _player.IsHit = true;
        _player.gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
    }

    public override void OnExecute()
    {
        _player.FieldTouchOperator.IsTouchLayerPath(out string[] aa);
        _timer += Time.deltaTime;
        _playerStateData.Rigid.SetMoveDirection = new Vector2(_dir * (_playerStateData.Status.Speed * 2), 0);
        if (_playerStateData.DodgeTime <= _timer)
        {
            _isEnd = true;
        }
    }

    public override Enum OnExit()
    {
        if (_playerStateData.ReadMoveDirection == Vector2.zero && _isEnd)
        {
            return Player.PlayerState.Idle;
        }
        if (_playerStateData.ReadMoveDirection.x != 0 && _isEnd)
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
        return Player.PlayerState.Dodge;
    }

    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
        _playerStateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
    }
}
