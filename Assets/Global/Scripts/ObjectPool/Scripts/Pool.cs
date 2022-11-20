using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool.Event;

namespace ObjectPool
{
    /// <summary>
    /// ObjectPoolの管理クラス
    /// </summary>
    /// <typeparam name="MonoPool">Poolにする対象Object</typeparam>
    public class Pool<MonoPool> where MonoPool : MonoBehaviour, IPool
    {
        /// <summary>
        /// 各Poolのデータ
        /// </summary>
        class PoolData
        {
            public MonoPool Pool { get; set; }
            public IPoolOnEnableEvent OnEnable { get; set; }
            public IPoolEvent Event { get; set; }
            public IPoolDispose Dispose { get; set; }
            public bool IsUse { get; set; }

            /// <summary>
            /// Eventの終了通知
            /// </summary>
            /// <returns></returns>
            public bool IsEvent()
            {
                if (Event == null)
                {
                    return false;
                }

                return Event.IsDone;
            }

            public void DisposeEvent()
            {
                if (Dispose != null)
                {
                    Dispose.Dispose();
                }
            }
        }

        int _createCount;
        bool _isCreate;
        bool _isSetMono;

        MonoPool _monoPool;
        List<PoolData> _poolList = new List<PoolData>();

        Transform _parent;

        /// <summary>
        /// 初期設定
        /// </summary>
        /// <param name="pool">Poolにする対象</param>
        /// <param name="createCount">生成数</param>
        public Pool<MonoPool> SetMono(MonoPool pool, int createCount = 5)
        {
            _createCount = createCount;
            _monoPool = pool;
            
            _isSetMono = true;

            return this;
        }

        /// <summary>
        /// 親の保持
        /// </summary>
        /// <param name="parent">親にするTransform</param>
        /// <returns></returns>
        public Pool<MonoPool> IsSetParent(Transform parent)
        {
            _parent = parent;
            
            return this;
        }

        /// <summary>
        /// Poolの生成Request
        /// </summary>
        /// <returns></returns>
        public void CreateRequest()
        {
            CreatePool(_createCount);
            _isCreate = true;
        }

        /// <summary>
        /// Poolの生成
        /// </summary>
        /// <param name="createCount">生成数</param>
        void CreatePool(int createCount)
        {
            for (int index = 0; index < createCount; index++)
            {
                MonoPool pool = Object.Instantiate(_monoPool);
                pool.Setup(_parent);

                PoolData data = CreateData(pool);
                _poolList.Add(data);

                data.Pool.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Poolデータの作成
        /// </summary>
        /// <param name="pool">対象Pool</param>
        /// <returns>PoolData</returns>
        PoolData CreateData(MonoPool pool)
        {
            PoolData data = new PoolData();
            data.Pool = pool;
            data.IsUse = false;

            data.Event = pool.GetComponent<IPoolEvent>();
            data.Dispose = pool.GetComponent<IPoolDispose>();
            data.OnEnable = pool.GetComponent<IPoolOnEnableEvent>();

            if (_parent != null)
            {
                data.Pool.transform.SetParent(_parent);
            }

            return data;
        }

        /// <summary>
        /// Poolの使用リクエスト
        /// </summary>
        /// <param name="action">Invoke(); することで使用</param>
        /// <returns>対象Pool</returns>
        public MonoPool UseRequest(out System.Action action)
        {
            action = null;

            if (!ChackSuccess())
            {
                Debug.LogWarning("Poolの使用権限がありません");
                return null;
            }

            try
            {
                PoolData data = _poolList.First(p => !p.IsUse);
                
                data.Pool.gameObject.SetActive(true);

                action += () => data.IsUse = true;
                if (data.Event != null)
                {
                    action += () => data.Event.IsDone = false;
                }
                if (data.OnEnable != null)
                {
                    action += () => data.OnEnable.OnEnableEvent();
                }
                action += () => data.Pool.StartCoroutine(Execution(data));

                return data.Pool;
            }
            catch
            {
                CreatePool(_createCount);

                Debug.LogWarning($"Poolが上限に達したので上限を増やしました。" +
                    $"\n 対象Pool.{_monoPool.name} : 生成数.{_createCount} : 上限.{_poolList.Count}");

                return UseRequest(out action);
            }
        }

        /// <summary>
        /// Poolの使用リクエスト。
        /// 呼ばれたタイミングでPoolを使用
        /// </summary>
        public MonoPool UseRequest()
        {
            System.Action action = null;

            if (!ChackSuccess())
            {
                Debug.LogWarning("Poolの使用権限がありません");
                return null;
            }

            try
            {
                PoolData data = _poolList.First(p => !p.IsUse);
                
                data.Pool.gameObject.SetActive(true);

                action += () => data.IsUse = true;
                if (data.Event != null)
                {
                    action += () => data.Event.IsDone = false;
                }
                if (data.OnEnable != null)
                {
                    action += () => data.OnEnable.OnEnableEvent();
                }
                action += () => data.Pool.StartCoroutine(Execution(data));

                action.Invoke();
                return data.Pool;
            }
            catch
            {
                CreatePool(_createCount);

                Debug.LogWarning($"Poolが上限に達したので上限を増やしました。" +
                    $"\n 対象Pool.{_monoPool.name} : 生成数.{_createCount} : 上限.{_poolList.Count}");

                return UseRequest();
            }
        }

        bool ChackSuccess()
        {
            if (!_isSetMono)
            {
                Debug.LogWarning("対象Poolがありません。");
                return false;
            }

            if (!_isCreate)
            {
                Debug.LogWarning($"Poolの生成Requestがされていないので使用できません。対象Pool {_monoPool.name}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 実行中の各Update
        /// </summary>
        /// <param name="data">対象のPoolデータ</param>
        /// <returns>Null</returns>
        IEnumerator Execution(PoolData data)
        {
            yield return null;

            while (!data.Pool.Execute() && !data.IsEvent())
            {
                // Debug.Log($"Execute {data.Pool.Execute()} Event {data.IsEvent()}");
                yield return null;
            }

            Delete(data);
        }

        void Delete(PoolData data)
        {
            if (_parent != null)
            {
                data.Pool.transform.SetParent(_parent);
            }

            data.IsUse = false;
            data.DisposeEvent();
            data.Pool.gameObject.SetActive(false);
        }
    }
}
