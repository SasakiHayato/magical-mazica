using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomPhysics.Data
{
    public enum ForceType
    {
        Impulse,
        VerticalThrow,
    }

    public class ForceModel
    {
        ForceData _forceData;

        public Vector2 Direction { get; private set; }

        public void SetData(ForceData data)
        {
            _forceData = data;
        }

        public void Process()
        {

        }
    }
}