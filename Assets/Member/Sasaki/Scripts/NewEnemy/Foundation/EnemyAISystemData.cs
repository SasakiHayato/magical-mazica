using System.Collections.Generic;
using UnityEngine;

namespace EnemyAISystem
{
    public interface ICondition
    {
        void Setup(Transform user);
        bool Try();
        void Initalize();
    }

    public interface IMove
    {
        void Setup(Transform user);
        Vector2 Execute();
        void Initalize();
    }

    [System.Serializable]
    public class EnemyAISystemData
    {
        [System.Serializable]
        public class StateData
        {
            [SerializeField] string _statePath;
            [SerializeField] List<ExitData> _exitDataList;
            [SerializeField] List<TaskData> _taskDataList;

            public string StatePath => _statePath;
            public List<ExitData> ExitDataList => _exitDataList;
            public List<TaskData> TaskDataList => _taskDataList;
        }

        [System.Serializable]
        public class ExitData
        {
            [SerializeField] string _nextStatePath;
            [SerializeReference, SubclassSelector]
            List<ICondition> _conditions;

            public string NextStatePath => _nextStatePath;
            public List<ICondition> ConditionList => _conditions;
        }

        [System.Serializable]
        public class TaskData
        {
            [SerializeField] float _taskIntervalTime;
            [SerializeReference, SubclassSelector]
            IMove _move;

            public float TaskIntervalTime => _taskIntervalTime;
            public IMove Move => _move;
        }

        [SerializeField] string _startStatePath;
        [SerializeField] float _stateIntervalTime;
        [SerializeField] List<StateData> _stateList;

        public string StartStatePath => _startStatePath;
        public float StateIntervalTime => _stateIntervalTime;
        public StateData GetStateData(string path) => _stateList.Find(s => s.StatePath == path);
        public void SetupUser(Transform user)
        {
            _stateList.ForEach(s =>
            {
                s.ExitDataList.ForEach(e => e.ConditionList.ForEach(c => c.Setup(user)));
                s.TaskDataList.ForEach(t => t.Move.Setup(user));
            });
        }
    }

}