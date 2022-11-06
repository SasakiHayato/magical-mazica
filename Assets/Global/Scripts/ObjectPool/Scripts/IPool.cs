namespace ObjectPool
{
    /// <summary>
    /// Pool������Object�Ɍp��
    /// </summary>
    public interface IPool
    {
        /// <summary>
        /// Start�֐�
        /// </summary>
        /// <param name="parent">Pool�̐e</param>
        void Setup(UnityEngine.Transform parent);

        /// <summary>
        /// �g�p����Update�֐�
        /// </summary>
        /// <returns>�g�p�I������Ture��Ԃ�</returns>
        bool Execute();
    }
}
