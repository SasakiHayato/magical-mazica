using UnityEngine;

namespace MonoState.Sample
{
    using MonoState.State;
    using System;

    public class Run : MonoStateAttribute
    {
        public override void Setup()
        {
            Debug.Log("Setup. Run");
        }

        public override void OnEnable()
        {
            Debug.Log("OnEnable. Run");
        }

        public override void Execute()
        {
            Debug.Log("Execute. Run");
        }

        public override Enum Exit()
        {
            return StateUser.State.Run;
        }
    }

}