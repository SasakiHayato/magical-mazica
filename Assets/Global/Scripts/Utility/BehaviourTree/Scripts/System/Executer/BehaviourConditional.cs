using UnityEngine;

namespace BehaviourTree.Execute
{
    /// <summary>
    /// Conditionを作成する際の基底クラス
    /// このクラスを派生してAIの行動条件の作成をする。
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

