using UnityEngine;
using MonoState.Data;

namespace MonoState.Sample
{
    public class SampleMonoStateData : IMonoDatableSystem<SampleMonoStateData>
    {
        string IMonoDatable.Path => nameof(SampleMonoStateData);
        SampleMonoStateData IMonoDatableSystem<SampleMonoStateData>.GetData => this;

        public bool OnJump { get; set; }
        public float InputAxisX => Input.GetAxisRaw("Horizontal");
        public Vector2 MoveDir { get; set; }
    }
}
