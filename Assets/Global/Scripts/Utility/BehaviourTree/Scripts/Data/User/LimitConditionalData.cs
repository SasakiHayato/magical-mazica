using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree.Data
{
    public class LimitConditionalData
    {
        public LimitConditionalData(int id, bool isCall)
        {
            ID = id;
            IsCall = isCall;
        }

        public int ID { get; private set; }
        public bool IsCall { get; private set; }

        public void CallBack()
        {
            IsCall = false;
        }
    }
}