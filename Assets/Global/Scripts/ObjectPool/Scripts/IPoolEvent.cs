namespace ObjectPool.Event
{
    /// <summary>
    /// PoolObject���C�ӂ̃^�C�~���O�ŏI���𔻒肵�����ꍇ�Ɍp��
    /// </summary>
    public interface IPoolEvent
    {
        /// <summary>
        /// Event�̏I���^�C�~���O��True��Ԃ��悤�ɂ���
        /// </summary>
        bool IsDone { get; set; }
    }

    /// <summary>
    /// PoolObject���j�������^�C�~���O��Event�𔭍s�������ꍇ�Ɍp��
    /// </summary>
    public interface IPoolDispose
    {
        void Dispose();
    }

    /// <summary>
    /// PoolObject�g�p����Execute�֐��O�̑O��Event�𔭍s����ꍇ�Ɍp��
    /// </summary>
    public interface IPoolOnEnableEvent
    {
        void OnEnableEvent();
    }
}