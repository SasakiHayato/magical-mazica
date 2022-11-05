using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MonoState.Sample
{
    using MonoState.State;

    public class Idle : MonoStateAttribute
    {
        public override void Setup()
        {
            Debug.Log("Setup. Idle");
        }

        public override void OnEnable()
        {
            Debug.Log("OnEnable. Idle");
            
            SampleData sample = UserRetentionData.GetData<SampleData>(nameof(SampleData));
            Debug.Log(sample.Data);
        }

        public override void Execute()
        {
            Debug.Log("Execute. Idle");
        }

        public override Enum Exit()
        {
            return StateUser.State.Idle;
        }
    }
}