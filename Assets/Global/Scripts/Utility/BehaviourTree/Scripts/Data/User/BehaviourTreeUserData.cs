using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Data
{
    /// <summary>
    /// BehaviorTreeUserのデータクラス
    /// </summary>
    public class BehaviourTreeUserData
    {
        public BehaviourTreeUserData(BehaviourTreeUser user, Transform transform, string path)
        {
            Offset = transform;
            User = user;
            UserPath = path;
        }

        public string UserPath { get; private set; }

        public string IOPath { get; private set; }

        public Transform Offset { get; private set; }

        public BehaviourTreeUser User { get; private set; }

        public List<LimitConditionalData> LimitConditionalList { get; private set; } = new List<LimitConditionalData>();

        public void SetLimitConditionalData(int id)
        {
            LimitConditionalData data = new LimitConditionalData(id, true);
            LimitConditionalList.Add(data);
        }

        public bool IsLimitCondition(int id)
        {
            LimitConditionalData data = LimitConditionalList.FirstOrDefault(l => l.ID == id);
            return data.IsCall;
        }

        public void IsCallLimit(int id)
        {
            LimitConditionalData data = LimitConditionalList.FirstOrDefault(l => l.ID == id);
            data.CallBack();
        }

        public void SetIOPath(string path) => IOPath = path;
    }
}
