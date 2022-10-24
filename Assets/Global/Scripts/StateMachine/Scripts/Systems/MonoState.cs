using UnityEngine;
using System;
using MonoState.Data;

namespace MonoState.State
{
    public abstract class MonoState
    {
        public UserRetentionData UserRetentionData { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="user"></param>
        public abstract void Setup(GameObject user);

        /// <summary>
        /// �X�e�[�g�ɓ��邽�тɌĂ΂��
        /// </summary>
        public abstract void OnEnable();
        public abstract void Execute();
        public abstract Enum Exit();
    }
}