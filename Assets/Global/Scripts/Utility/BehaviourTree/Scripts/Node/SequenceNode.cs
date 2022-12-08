using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Node
{
    /// <summary>
    /// 順番に実行を操作するノード。
    /// System.Linq All()の働き
    /// 
    /// Listが０又は、最後まで行った際に初期化を行う
    /// </summary>
    /// <typeparam name="Execution">Action又はCondition</typeparam>
   
    public class SequenceNode<Execution> : NodeBase where Execution : ExecuteBase
    {
        int _executeID;
        bool _isAll;

        List<Execution> _executeList;

        public SequenceNode(List<Execution> type, bool isExecuteAll = true) : base()
        {
            _executeList = type;
            _isAll = isExecuteAll;

           
        }

        public override void SetUp() 
        {
            _executeID = 0;
        }

        protected override bool Execute()
        {
            if (_executeList.Count <= 0 || _executeList.Count <= _executeID)
            {
                return true;
            }

            if (_isAll)
            {
                return _executeList.All(e => e.IsExecute);
            }
            else
            {
                return OnNext();
            }
        }

        bool OnNext()
        {
            if (_executeList[_executeID].IsExecute)
            {
                _executeID++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
