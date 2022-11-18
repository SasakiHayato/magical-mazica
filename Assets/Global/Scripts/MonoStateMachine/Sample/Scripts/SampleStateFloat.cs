using System;
using UnityEngine;
using MonoState.State;
using MonoState.Data;
using MonoState.Opration;

namespace MonoState.Sample
{
    public class SampleStateFloat : MonoStateBase
    {
        SampleMonoStateUser _stateUser;
        SampleMonoStateData _stateData;

        float _timer = 0;
        float _height = 0;
        float _gravity = 0;

        public override void Setup(MonoStateData data)
        {
            _stateUser = data.GetMonoDataUni<SampleMonoStateUser>(nameof(SampleMonoStateUser));
            _stateData = data.GetMonoData<SampleMonoStateData>(nameof(SampleMonoStateData));

            _gravity = Physics2D.gravity.y * -1;
        }
        public override void OnEntry()
        {
            _timer = 0;
            _height = 0;
        }

        public override void OnExecute()
        {
            _timer += Time.deltaTime;
            _height = 15 * _timer - (_gravity * _timer * _timer) / 2;

            Vector2 move = _stateData.MoveDir;
            move.y = _height;
            Debug.Log(_height);
            _stateData.MoveDir = move;
        }

        public override Enum OnExit()
        {
            if (_stateUser.IsGround() && _height < 0)
            {
                _stateData.OnJump = false;
                return ReturneDefault();
            }

            return SampleMonoStateUser.State.Float;
        }
    }
}
