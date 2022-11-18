using System;
using UnityEngine;
using MonoState.State;
using MonoState.Data;

namespace MonoState.Sample
{
    public class SampleStateIdle : MonoStateBase
    {
        SampleMonoStateUser _stateUser;
        SampleMonoStateData _stateData;

        public override void Setup(MonoStateData data)
        {
            _stateUser = data.GetMonoDataUni<SampleMonoStateUser>(nameof(SampleMonoStateUser));
            _stateData = data.GetMonoData<SampleMonoStateData>(nameof(SampleMonoStateData));
        }

        public override void OnEntry()
        {
            Vector2 move = Vector2.zero;
            move.y = Physics2D.gravity.y;

            _stateData.MoveDir = move;
        }

        public override void OnExecute()
        {
            
        }

        public override Enum OnExit()
        {
            if (_stateData.OnJump)
            {
                return SampleMonoStateUser.State.Float;
            }

            if (_stateData.InputAxisX != 0)
            {
                return SampleMonoStateUser.State.Move;
            }

            return SampleMonoStateUser.State.Idle;
        }
    }
}