using UnityEngine;
using MonoState.Data;

namespace MonoState.Sample
{
    public class SampleData : MonoBehaviour, IRetentionData
    {
        public string Data { get; set; }

        string IRetentionData.Path => nameof(SampleData);

        Object IRetentionData.RetentionData()
        {
            return this;
        }
    }
}