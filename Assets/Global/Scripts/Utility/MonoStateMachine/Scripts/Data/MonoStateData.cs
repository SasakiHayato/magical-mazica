using System.Collections.Generic;
using UnityEngine;

namespace MonoState.Data
{
    /// <summary>
    /// ステートマシンに保存するデータクラス
    /// </summary>
    public class MonoStateData
    {
        List<IMonoDatable> _monoDataList = new List<IMonoDatable>();

        public Transform StateUser { get; set; }

        /// <summary>
        /// データの追加
        /// </summary>
        /// <param name="datable">IMonoDatableを継承したインターフェース</param>
        public void AddMonoData(IMonoDatable datable)
        {
            _monoDataList.Add(datable);
        }

        /// <summary>
        /// データの取得。UnityEngne.Objectを継承されてないものが対象
        /// </summary>
        /// <typeparam name="MonoData">IMonoDatableSystemを継承したクラス</typeparam>
        /// <param name="path">対象パス</param>
        /// <returns></returns>
        public MonoData GetMonoData<MonoData>(string path) where MonoData : IMonoDatable
        {
            try
            {
                IMonoDatable monoDatable = _monoDataList.Find(m => m.Path == path);
                IMonoDatableSystem<MonoData> system = monoDatable as IMonoDatableSystem<MonoData>;

                return (MonoData)system;
            }
            catch
            {
                return default;
            }
            
        }

        /// <summary>
        /// データの取得。UnityEngne.Objectを継承しているものが対象
        /// </summary>
        /// <typeparam name="MonoData">IMonoDatableUniを継承したクラス</typeparam>
        /// <param name="path">対象パス</param>
        /// <returns></returns>
        public MonoData GetMonoDataUni<MonoData>(string path) where MonoData : Object, IMonoDatable
        {
            try
            {
                IMonoDatable monoDatable = _monoDataList.Find(m => m.Path == path);
                IMonoDatableUni<MonoData> uni = monoDatable as IMonoDatableUni<MonoData>;

                return (MonoData)uni;
            }
            catch
            {
                return null;
            }
            
        }
    }
}
