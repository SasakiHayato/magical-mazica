namespace MonoState.Data
{
    /// <summary>
    /// データ保存のための基底インターフェース
    /// </summary>
    public interface IMonoDatable
    {
        string Path { get; }
    }

    /// <summary>
    /// UnityEngne.Objectを継承していないクラスに適応
    /// </summary>
    /// <typeparam name="Data"></typeparam>
    public interface IMonoDatableSystem<Data> : IMonoDatable
    {
        Data GetData { get; }
    }

    /// <summary>
    /// UnityEngne.Objectを継承しているクラスに適応
    /// </summary>
    /// <typeparam name="Data"></typeparam>
    public interface IMonoDatableUni<Data> : IMonoDatable where Data : UnityEngine.Object  
    {
        Data GetData { get; }
    }
}