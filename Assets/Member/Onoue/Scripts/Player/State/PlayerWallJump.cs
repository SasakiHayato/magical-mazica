using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MonoState.State;
using MonoState.Data;

public class PlayerWallJump : MonoStateBase
{
    Player _player;
    //Stateが変わる度に呼ばれる
    public override void OnEntry()
    {
        //_player.Rigidbody.velocity = Vector2.zero;
        _player.RigidOperate.InitalizeGravity();
        _player.FieldTouchOperator.IsTouchLayerID(out int[] id);
        if (id[0] == 2)
        {
            _player.RigidOperate.SetImpulse(_player.WallJumpPower, RigidMasterData.ImpulseDirectionType.Vertical);
            _player.RigidOperate.SetImpulse(_player.WallJumpX[1], RigidMasterData.ImpulseDirectionType.Horizontal);
            //_player.RigidOperate.AddForce(new Vector2(-1, 1) * _player.WallJumpPower, ForceMode2D.Impulse);
        }
        else if (id[0] == 3)
        {
            _player.RigidOperate.SetImpulse(_player.WallJumpPower, RigidMasterData.ImpulseDirectionType.Vertical);
            _player.RigidOperate.SetImpulse(_player.WallJumpX[0], RigidMasterData.ImpulseDirectionType.Horizontal);
            //_player.RigidOperate.AddForce(Vector2.one * _player.WallJumpPower, ForceMode2D.Impulse);
        }
    }
    //Update
    public override void OnExecute()
    {

    }
    //条件分岐
    public override Enum OnExit()
    {
        if (!_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))//&& !_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true)
        {
            if (_player.RigidOperate.ReadVelocity.y < 0)
            {
                _player.IsWallJumped = false;
                return Player.PlayerState.Float;
            }
        }
        //ここが原因
        //if (_player.FieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))//&& MathF.Abs(_player.RigidOperate.ReadVelocity.x) <= 0
        //{
        //    if (!_player.IsWallJumped)
        //    {
        //        _player.IsWallJumped = false;
        //        return Player.PlayerState.IsStick;
        //    }
        //}
        return Player.PlayerState.WallJump;
    }

    //Awake
    public override void Setup(MonoStateData data)
    {
        _player = data.GetMonoDataUni<Player>(nameof(Player));
    }
}

