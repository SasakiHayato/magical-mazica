using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Node
{
    /// <summary>
    /// �ǂꂩ�̎��s�𑀍삷��m�[�h
    /// 
    /// System.Linq Any()�̓���
    /// </summary>
    /// <typeparam name="Execution"></typeparam>
    
    public class SelectorNode<Execution> : NodeBase where Execution : ExecuteBase
    {
        List<Execution> _executeList;
        public SelectorNode(List<Execution> type)
        {
            _executeList = type;
        }

        public override void SetUp() { }

        protected override bool Execute()
        {
            return _executeList.Any(e => e.IsExecute);
        }
    }
}
