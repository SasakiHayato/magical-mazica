namespace BehaviourTree
{
    /// <summary>
    /// Action‚ÆCondition‚ğì¬‚·‚éÛ‚ÌŠî’êƒNƒ‰ƒX
    /// </summary>
    public abstract class ExecuteBase
    {
        public bool IsExecute => BaseExecute();

        public abstract void BaseInit();

        public abstract void BaseSetup(UnityEngine.GameObject user);

        protected abstract bool BaseExecute();
    }
}