using UnityEngine;
using MonoState.Data;

namespace MonoState.Sample
{
    public class SampleData : MonoBehaviour, IRetentionData
    {
        public string Data { get; set; }

        public Object RetentionData()
        {
            return this;
        }
    }
}