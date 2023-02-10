using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerWallJump : MonoStateBase
{
    Player _player;
    PlayerStateData _playerStateData;
    AnimOperator _anim;
    //StateÇ™ïœÇÌÇÈìxÇ…åƒÇŒÇÍÇÈ
    public override void OnEntry()
    {
        _anim.OnPlay("Jump");

        //_player.Rigidbody.velocity = Vector2.zero;
        _playerStateData.Rigid.InitalizeGravity();
        _player.FieldTouchOperator.IsTouchLayerID(out int[] id);
        if (id[0] == 2)
        {
            _playerStateData.Rigid.SetImpulse(_playerStateData.Jump.WallPower.y, RigidMasterData.ImpulseDirectionType.Vertical);
            _playerStateData.Rigid.SetImpulse(_playerStateData.Jump.WallPower.x * -1, RigidMasterData.ImpulseDirectionType.Horizontal);
            
        }
        else if (id[0] == 3)
        {
            _playerStateData.Rigid.SetImpulse(_playerStateData.Jump.WallPower.y, RigidMasterData.ImpulseDirectionType.Vertical);
            _playerStateData.Rigid.SetImpulse(_playerStateData.Jump.WallPower.x, RigidMasterData.ImpulseDirectionType.Horizontal);
        }
    }
    //Update
    public override void OnExecute()
    {

    }
    //èåèï™äÚ
    public override Enum OnExit()
    {
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))//&& !_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true)
        {
            if (_playerStateData.Rigid.ReadVelocity.y < 0)
            {
                return Player.PlayerState.Float;
            }

            return Player.PlayerState.WallJump;
        }
        else
        {
            return Player.PlayerState.Idle;
        }
        //Ç±Ç±Ç™å¥àˆ
        //if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))//&& MathF.Abs(_player.RigidOperate.ReadVelocity.x) <= 0
        //{
        //    if (!_player.IsWallJumped)
        //    {
        //        _player.IsWallJumped = false;
        //        return Player.PlayerState.IsStick;
        //    }
        //}
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
        _playerStateData = data.GetMonoData<PlayerStateData>(nameof(PlayerStateData));
        _anim = data.GetMonoDataUni<AnimOperator>(nameof(AnimOperator));
    }
}

