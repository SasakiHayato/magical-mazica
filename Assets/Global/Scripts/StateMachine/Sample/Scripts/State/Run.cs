using UnityEngine;

namespace MonoState.Sample
{
    using MonoState.State;
    using System;

    public class Run : MonoState
    {
        public override void Setup(GameObject user)
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