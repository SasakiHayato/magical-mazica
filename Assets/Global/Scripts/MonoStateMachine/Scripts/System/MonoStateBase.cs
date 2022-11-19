using MonoState.Data;

namespace MonoState.State
{
    /// <summary>
    /// �e�X�e�[�g�̊��N���X
    /// </summary>
    public abstract class MonoStateBase
    {
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="data"></param>
        public abstract void Setup(MonoStateData data);
        /// <summary>
        /// �X�e�[�g�ɓ��邽�тɈ�x�����Ă΂��
        /// </summary>
        public abstract void OnEntry();
        /// <summary>
        /// Update�֐�
        /// </summary>
        public abstract void OnExecute();
        /// <summary>
        /// ���̃X�e�[�g��ݒ肷��
        /// </summary>
        /// <returns></returns>
        public abstract System.Enum OnExit();
        /// <summary>
        /// �����X�e�[�g��Ԃ�
        /// </summary>
        /// <returns></returns>
        protected System.Enum ReturneDefault()
        {
            return default;
        }
    }
}
