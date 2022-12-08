using UnityEngine;

namespace BehaviourTree.Node
{
    /// <summary>
    /// �m�[�h�쐬�̍ۂ̊��N���X
    /// </summary>

    public abstract class NodeBase
    {
        public GameObject User { get; private set; }

        public void SetNodeUser(GameObject user) => User = user;

        public virtual void Init() { }

        public bool IsProcess => Execute();

        public abstract void SetUp();

        protected abstract bool Execute();
    }
}
