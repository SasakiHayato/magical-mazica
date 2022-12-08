using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;

namespace BehaviourTree.Node
{
    /// <summary>
    /// Actionを行うノード
    /// 
    /// 行動終了時、Actionの初期化を行う
    /// </summary>
    [System.Serializable]
    public class ActionNode : NodeBase
    {
        [SerializeReference, SubclassSelector]
        List<BehaviourAction> _actionList;

        SequenceNode<BehaviourAction> _sequenceNode;

        int _conut;

        public override void SetUp()
        {
            if (_actionList == null || _actionList.Count <= 0)
            {
                return;
            }

            _actionList.ForEach(a => a.BaseSetup(User));
            _sequenceNode = new SequenceNode<BehaviourAction>(_actionList, false);
        }

        protected override bool Execute()
        {
            if (_sequenceNode.IsProcess)
            {
                _conut++;
            }

            if (_conut >= _actionList.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Init()
        {
            base.Init();
            
            _conut = 0;
            _actionList.ForEach(a => a.BaseInit());
            _sequenceNode.SetUp();
        }
    }
}