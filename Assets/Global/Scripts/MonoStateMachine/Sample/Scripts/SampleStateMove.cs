using System;
using UnityEngine;
using MonoState.State;
using MonoState.Data;

namespace MonoState.Sample
{
    public class SampleStateMove : MonoStateBase
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

        }

        public override void OnExecute()
        {
            Vector2 move = Vector2.right * _stateData.InputAxisX;
            _stateData.MoveDir = move;
        }

        public override Enum OnExit()
        {
            if (_stateData.OnJump)
            {
                return SampleMonoStateUser.State.Float;
            }

            if (_stateData.InputAxisX == 0)
            {
                return SampleMonoStateUser.State.Idle;
            }
            
            return SampleMonoStateUser.State.Move;
        }
    }
}
