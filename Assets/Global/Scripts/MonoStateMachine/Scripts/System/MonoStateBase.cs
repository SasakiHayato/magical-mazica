using MonoState.Data;

namespace MonoState.State
{
    public enum MonoStateType
    {
        ReturneDefault,
        ReEntry,
    }

    /// <summary>
    /// 各ステートの基底クラス
    /// </summary>
    public abstract class MonoStateBase
    {
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="data"></param>
        public abstract void Setup(MonoStateData data);
        /// <summary>
        /// ステートに入るたびに一度だけ呼ばれる
        /// </summary>
        public abstract void OnEntry();
        /// <summary>
        /// Update関数
        /// </summary>
        public abstract void OnExecute();
        /// <summary>
        /// 次のステートを設定する
        /// </summary>
        /// <returns></returns>
        public abstract System.Enum OnExit();
        /// <summary>
        /// 初期ステートを返す
        /// </summary>
        /// <returns></returns>
        protected System.Enum ReturneState(MonoStateType stateType = MonoStateType.ReturneDefault)
        {
            return stateType;
        }
    }
}
