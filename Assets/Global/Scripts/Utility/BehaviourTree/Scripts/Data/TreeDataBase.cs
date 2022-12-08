using UnityEngine;
using System.Collections.Generic;
using BehaviourTree.Node;

namespace BehaviourTree.Data
{
    /// <summary>
    /// AI�s���̃f�[�^�쐬�N���X
    /// </summary>
    
    [System.Serializable]
    public class TreeDataBase 
    {
        [SerializeField] string _name;
        [SerializeField] ConditionalNode _mastarCodition;
        [SerializeField] TreeData _treeData;
        
        List<NodeBase> _nodeList;

        public int ID { get; set; } = -999;

        /// <summary>
        /// �����̏����̐��ۂ�Ԃ�
        /// </summary>
        public bool IsAccess => _mastarCodition.IsProcess;

        public bool HasCondition => _mastarCodition.HasCondition;

        public TreeData TreeData => _treeData;

        public List<NodeBase> NodeList
        {
            get
            {
                if (_nodeList == null)
                {
                    _nodeList = new List<NodeBase>();
                    AddNode();
                }

                return _nodeList;
            }
        }

        void AddNode()
        {
            _nodeList.Add(_mastarCodition);
            foreach (ExecuteData data in _treeData.ExecuteList)
            {
                _nodeList.Add(data.Action);
                _nodeList.Add(data.Condition);
            }
        }
    }
}

