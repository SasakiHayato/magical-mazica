using UnityEngine;

namespace BehaviourTree.Execute
{
    /// <summary>
    /// Condition���쐬����ۂ̊��N���X
    /// ���̃N���X��h������AI�̍s�������̍쐬������B
    /// </summary>
    [System.Serializable]
    public abstract class BehaviourConditional : ExecuteBase
    {
        protected GameObject User { get; private set; }

        public override void BaseInit() => Initialize();

        public override void BaseSetup(GameObject user) => Setup(user);

        protected virtual void Setup(GameObject user) 
        {
            User = user;
        }

        protected abstract bool Try();

        protected override bool BaseExecute() => Try();

        protected virtual void Initialize() { }
    }
}

