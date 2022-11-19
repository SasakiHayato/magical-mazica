using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using MonoState.State;
using MonoState.Data;
using MonoState.Opration;

namespace MonoState
{
    /// <summary>
    /// ステートマシンの管理クラス
    /// </summary>
    /// <typeparam name="User">使用者</typeparam>
    public class MonoStateMachine<User> where User : MonoBehaviour
    {
        float _timer = 0;
        float _intervalTime = 0;
        bool _isRun = false;

        MonoStateBase _currentState = null;
        Dictionary<string, MonoStateBase> _stateDic = new Dictionary<string, MonoStateBase>();
        MonoStateData _data = new MonoStateData();

        User _user;

        /// <summary>
        /// ステートマシンを使用するかどうか
        /// </summary>
        public bool IsRun
        {
            get => _isRun;

            set
            {
                _isRun = value;
                SetRun(value);
            }
        }
        /// <summary>
        /// 現在のステートキー
        /// </summary>
        public string CurrentKey { get; private set; } = null;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="user">使用者</param>
        /// <param name="intervalTime">遷移するためのインターバル</param>
        public MonoStateMachine(User user, float intervalTime = 0)
        {
            _user = user;
            _data.StateUser = user.transform;
            _intervalTime = intervalTime;
        }

        /// <summary>
        /// ステートの追加
        /// </summary>
        /// <param name="stateKey">ステートキー</param>
        /// <param name="state">ステートの実体</param>
        /// <returns></returns>
        public MonoStateMachine<User> AddState(System.Enum stateKey, MonoStateBase state)
        {
            _stateDic.Add(stateKey.ToString(), state);
            return this;
        }

        /// <summary>
        /// ステートで使用するデータの追加
        /// </summary>
        /// <param name="datable">IMonoDatableを継承されたインターフェース</param>
        /// <returns></returns>
        public MonoStateMachine<User> AddMonoData(IMonoDatable datable)
        {
            _data.AddMonoData(datable);
            return this;
        }

        void SetRun(bool isRun)
        {
            System.Action action = isRun ? Setup : () => Object.Destroy(_user.GetComponent<MonoStateOperator>());
            action.Invoke();
        }

        void Setup()
        {
            foreach (MonoStateBase state in _stateDic.Values)
            {
                state.Setup(_data);
            }

            OnNext(_stateDic.Keys.First());

            MonoStateOperator monoStateOperator = _user.AddComponent<MonoStateOperator>();
            monoStateOperator.Run = OnProcess;
        }

        void OnProcess()
        {
            _currentState.OnExecute();

            _timer += Time.deltaTime;
            if (_timer < _intervalTime) return;
            
            string nextState = _currentState.OnExit()?.ToString();

            if (nextState == default)
            {
                OnNext(_stateDic.Keys.First());
                return;
            }
            
            if (nextState != CurrentKey)
            {
                OnNext(nextState);
            }
        }

        void OnNext(string nextState)
        {
            MonoStateBase stateBase = _stateDic.First(s => s.Key == nextState).Value;
            stateBase.OnEntry();

            _currentState = stateBase;
            CurrentKey = nextState;

            _timer = 0;
        }
    }
}