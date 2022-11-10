using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoState.Data;
namespace MonoState.Player
{
    public class NewPlayer : MonoBehaviour, IRetentionData
    {
        public string TestData { get; set; }
        public Object RetentionData()
        {
            throw new System.NotImplementedException();
        }
    }
}
