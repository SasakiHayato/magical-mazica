using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;
using BehaviourTree.Data;

namespace BehaviourTree.Node
{
    /// <summary>
    /// Conditionを判定するノード
    /// 
    /// 毎判定時、Conditionを初期化を行う
    /// </summary>
    
    [System.Serializable]
    public class ConditionalNode : NodeBase
    {
        [SerializeField] ConditionType _conditionType;

        [SerializeReference, SubclassSelector]
        List<BehaviourConditional> _couditionList;

        SelectorNode<BehaviourConditional> _selectorNode;
        SequenceNode<BehaviourConditional> _sequenceNode;

        public bool HasCondition { get; private set; }

        public override void SetUp()
        {
            if (_couditionList == null || _couditionList.Count <= 0)
            {
                HasCondition = false;
                return;
            }
            else
            {
                HasCondition = true;
            }

            _couditionList.ForEach(c => c.BaseSetup(User));

            _selectorNode = new SelectorNode<BehaviourConditional>(_couditionList);
            _sequenceNode = new SequenceNode<BehaviourConditional>(_couditionList);
        }

        protected override bool Execute()
        {
            bool isExecute = false;

            if (_couditionList == null || _couditionList.Count <= 0)
            {
                return true;
            }

            switch (_conditionType)
            {
                case ConditionType.Selector: isExecute = _selectorNode.IsProcess; break;
                case ConditionType.Sequence: isExecute = _sequenceNode.IsProcess; break;
            }

            _couditionList.ForEach(c => c.BaseInit());

            return isExecute;
        }
    }
}