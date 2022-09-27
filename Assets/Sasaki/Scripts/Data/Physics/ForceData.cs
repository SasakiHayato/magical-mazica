using UnityEngine;

namespace CustomPhysics.Data
{
    public struct ForceData
    {
        public Vector2 Direction { get; private set; }

        public ForceType ForceType { get; private set; }

        public ForceData(Vector2 dir, ForceType type)
        {
            Direction = dir;
            ForceType = type;
        }

        public ForceData SendData()
        {
            return this;
        }
    }
}
