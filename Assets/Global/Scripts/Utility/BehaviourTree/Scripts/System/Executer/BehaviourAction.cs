using UnityEngine;

namespace BehaviourTree.Execute
{
    /// <summary>
    /// Actionを作成する際の基底クラス。
    /// このクラスを派生してAI行動を作成する。
    /// </summary>
    [System.Serializable]
    public abstract class BehaviourAction : ExecuteBase
    {
        protected GameObject User { get; private set; }

        public override void BaseSetup(GameObject user) => Setup(user);

        public override void BaseInit() => Initialize();

        protected virtual void Setup(GameObject user) 
        {
            User = user;
        }

        protected abstract bool Execute();

        protected override bool BaseExecute() => Execute();

        protected virtual void Initialize() { }
    }
}