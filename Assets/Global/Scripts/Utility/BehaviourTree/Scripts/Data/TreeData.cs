using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree.Data
{
    [System.Serializable]
    public class TreeData
    {
        [SerializeField] ConditionType _treeType;
        [SerializeField] List<ExecuteData> _executeList;

        public ConditionType TreeType => _treeType;
        public List<ExecuteData> ExecuteList => _executeList;
    }
}