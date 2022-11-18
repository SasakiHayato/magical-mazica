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
    /// �X�e�[�g�}�V���̊Ǘ��N���X
    /// </summary>
    /// <typeparam name="User">�g�p��</typeparam>
    public class MonoStateMachine<User> where User : Object
    {
        float _timer = 0;
        float _intervalTime = 0;
        bool _isRun = false;

        MonoStateBase _currentState = null;
        Dictionary<string, MonoStateBase> _stateDic = new Dictionary<string, MonoStateBase>();
        MonoStateData _data = new MonoStateData();

        User _user;

        /// <summary>
        /// �X�e�[�g�}�V�����g�p���邩�ǂ���
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
        /// ���݂̃X�e�[�g�L�[
        /// </summary>
        public string CurrentKey { get; private set; } = null;

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="user">�g�p��</param>
        /// <param name="intervalTime">�J�ڂ��邽�߂̃C���^�[�o��</param>
        public MonoStateMachine(User user, float intervalTime = 0)
        {
            _user = user;
            _intervalTime = intervalTime;
        }

        /// <summary>
        /// �X�e�[�g�̒ǉ�
        /// </summary>
        /// <param name="stateKey">�X�e�[�g�L�[</param>
        /// <param name="state">�X�e�[�g�̎���</param>
        /// <returns></returns>
        public MonoStateMachine<User> AddState(System.Enum stateKey, MonoStateBase state)
        {
            _stateDic.Add(stateKey.ToString(), state);
            return this;
        }

        /// <summary>
        /// �X�e�[�g�Ŏg�p����f�[�^�̒ǉ�
        /// </summary>
        /// <param name="datable">IMonoDatable���p�����ꂽ�C���^�[�t�F�[�X</param>
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