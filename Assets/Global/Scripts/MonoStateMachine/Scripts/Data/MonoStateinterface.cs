namespace MonoState.Data
{
    /// <summary>
    /// �f�[�^�ۑ��̂��߂̊��C���^�[�t�F�[�X
    /// </summary>
    public interface IMonoDatable
    {
        string Path { get; }
    }

    /// <summary>
    /// UnityEngne.Object���p�����Ă��Ȃ��N���X�ɓK��
    /// </summary>
    /// <typeparam name="Data"></typeparam>
    public interface IMonoDatableSystem<Data> : IMonoDatable
    {
        Data GetData { get; }
    }

    /// <summary>
    /// UnityEngne.Object���p�����Ă���N���X�ɓK��
    /// </summary>
    /// <typeparam name="Data"></typeparam>
    public interface IMonoDatableUni<Data> : IMonoDatable where Data : UnityEngine.Object  
    {
        Data GetData { get; }
    }
}