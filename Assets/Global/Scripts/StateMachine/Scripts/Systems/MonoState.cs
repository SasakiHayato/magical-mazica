using UnityEngine;
using System;
using MonoState.Data;

namespace MonoState.State
{
    public abstract class MonoStateAttribute
    {
        public UserRetentionData UserRetentionData { get; set; }

        public GameObject User { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="user"></param>
        public abstract void Setup();

        /// <summary>
        /// ステートに入るたびに呼ばれる
        /// </summary>
        public abstract void OnEnable();
        public abstract void Execute();
        public abstract Enum Exit();
    }
}