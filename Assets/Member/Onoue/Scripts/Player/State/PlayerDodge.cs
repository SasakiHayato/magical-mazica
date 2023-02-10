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
    GameObject _obj;
    float _dir;
    float _timer;
    float _acceleration;
    bool _isEnd;
    Color _color;
    void IStateExitEventable.ExitEvent()
    {
        _player.gameObject.layer = LayerMask.NameToLayer("Player");
        _player.IsHit = false;
        _isEnd = false;
        _timer = 0;
        _color.a = 1;
        _player.Renderer.color = _color;
        _playerStateData.Rigid.MaxAcceleration = _acceleration;
        Time.timeScale = 1;
        _player.DodgeImage(_obj);
    }
    public override void OnEntry()
    {
        _anim.OnPlay("Dodge");
        _color = _player.Renderer.color;
        _color.a = 0.5f;
        _player.Renderer.color = _color;
        _obj = _player.DodgeImage(_obj);
        Time.timeScale = 2;
        _dir = _player.transform.localScale.x;
        _player.IsHit = true;

        _playerStateData.Rigid.SetMoveDirection = Vector2.zero;
        _acceleration = _playerStateData.Rigid.MaxAcceleration;
        _playerStateData.Rigid.MaxAcceleration = 0;

        _player.gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
    }

    public override void OnExecute()
    {
        _player.FieldTouchOperator.IsTouchLayerPath(out string[] aa);
        _timer += Time.deltaTime;
        _playerStateData.Rigid.SetMoveDirection = new Vector2(_dir * (_playerStateData.Status.Speed * 3), 0);
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
            if (_playerStateData.Rigid.ReadVelocity.y < 0 && _isEnd)
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
