using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Node
{
    /// <summary>
    /// ‚Ç‚ê‚©‚ÌÀs‚ğ‘€ì‚·‚éƒm[ƒh
    /// 
    /// System.Linq Any()‚Ì“­‚«
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
