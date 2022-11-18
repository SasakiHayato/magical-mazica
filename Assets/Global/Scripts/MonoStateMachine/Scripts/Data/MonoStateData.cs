using System.Collections.Generic;
using UnityEngine;

namespace MonoState.Data
{
    /// <summary>
    /// �X�e�[�g�}�V���ɕۑ�����f�[�^�N���X
    /// </summary>
    public class MonoStateData
    {
        List<IMonoDatable> _monoDataList = new List<IMonoDatable>();

        /// <summary>
        /// �f�[�^�̒ǉ�
        /// </summary>
        /// <param name="datable">IMonoDatable���p�������C���^�[�t�F�[�X</param>
        public void AddMonoData(IMonoDatable datable)
        {
            _monoDataList.Add(datable);
        }

        /// <summary>
        /// �f�[�^�̎擾�BUnityEngne.Object���p������ĂȂ����̂��Ώ�
        /// </summary>
        /// <typeparam name="MonoData">IMonoDatableSystem���p�������N���X</typeparam>
        /// <param name="path">�Ώۃp�X</param>
        /// <returns></returns>
        public MonoData GetMonoData<MonoData>(string path) where MonoData : IMonoDatable
        {
            IMonoDatable monoDatable = _monoDataList.Find(m => m.Path == path);
            IMonoDatableSystem<MonoData> system = monoDatable as IMonoDatableSystem<MonoData>;

            return (MonoData)system;
        }

        /// <summary>
        /// �f�[�^�̎擾�BUnityEngne.Object���p�����Ă�����̂��Ώ�
        /// </summary>
        /// <typeparam name="MonoData">IMonoDatableUni���p�������N���X</typeparam>
        /// <param name="path">�Ώۃp�X</param>
        /// <returns></returns>
        public MonoData GetMonoDataUni<MonoData>(string path) where MonoData : Object, IMonoDatable
        {
            IMonoDatable monoDatable = _monoDataList.Find(m => m.Path == path);
            IMonoDatableUni<MonoData> uni = monoDatable as IMonoDatableUni<MonoData>;

            return (MonoData)uni;
        }
    }
}
