using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


namespace MonoState
{
    using MonoState.State;
    using MonoState.Data;

    public class MonoStateMachine<User> where User : MonoBehaviour
    {
        User _user;

        bool _isRun;

        Dictionary<string, MonoState> _stateDic;
        CurrentMonoStateData _currentMonoState;

        UserRetentionData _userRetentionData;

        class CurrentMonoStateData
        {
            public string Path { get; private set; }
            public MonoState MonoState { get; private set; }

            public void SetPath(string path)
            {
                Path = path;
            }

            public void SetMonoState(MonoState state)
            {
                MonoState = state;
            }
        }
        
        public void Initalize(User user)
        {
            _user = user;
            _stateDic = new Dictionary<string, MonoState>();

            StateOperator stateOperator = user.gameObject.AddComponent<StateOperator>();
            stateOperator.Setup(() => Run());

            _currentMonoState = new CurrentMonoStateData();
            _userRetentionData = new UserRetentionData();
        }

        public MonoStateMachine<User> AddState(MonoState state, Enum path)
        {
            _stateDic.Add(path.ToString(), state);
            state.Setup(_user.gameObject);

            return this;
        }

        public MonoStateMachine<User> SetData(IRetentionData data)
        {
            _userRetentionData.SetData(data);

            return this;
        }

        public void SetRunRequest(Enum path)
        {
            MonoState monoState = _stateDic.First(d => d.Key == path.ToString()).Value;
            
            _currentMonoState.SetMonoState(monoState);
            _currentMonoState.SetPath(path.ToString());

            foreach (MonoState state in _stateDic.Values)
            {
                state.UserRetentionData = _userRetentionData;
            }

            monoState.OnEnable();

            _isRun = true;
        }

        void Run()
        {
            if (!_isRun)
            {
                Debug.Log($"RunRequest‚ª‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB‘ÎÛ => {_user.name}");
                return;
            }

            string path = _currentMonoState.MonoState.Exit().ToString();

            if (_currentMonoState.Path == path)
            {
                _currentMonoState.MonoState.Execute();
            }
            else
            {
                ChangeState(path);
                _currentMonoState.MonoState.OnEnable();
            }
        }

        public void ChangeState(string path)
        {
            MonoState monoState = _stateDic.First(d => d.Key == path).Value;

            _currentMonoState.SetMonoState(monoState);
            _currentMonoState.SetPath(path);
        }
    }
}