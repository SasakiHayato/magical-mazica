using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MonoState.Sample
{
    using MonoState.State;

    public class Move : MonoStateAttribute
    {
        public override void Setup()
        {
            Debug.Log("Setup. Move");
        }

        public override void OnEnable()
        {
            Debug.Log("OnEnable. Move");
        }

        public override void Execute()
        {
            Debug.Log("Execute. Move");
        }

        public override Enum Exit()
        {
            return StateUser.State.Move;
        }
    }
}