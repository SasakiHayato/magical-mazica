using System.Linq;
using System.Collections;
using UnityEngine;


namespace EnemyAISystem
{
    using static EnemyAISystemData;
    using static EnemyAIAttackData;

    public class EnemyAI : MonoBehaviour
    {
        class IntervalData
        {
            public float StateIntervalTime { get; private set; }
            public float TaskIntervalTime { get; private set; }

            public void AddTime()
            {
                float time = Time.deltaTime;

                StateIntervalTime += time;
                TaskIntervalTime += time;
            }

            public void InitalizeStateInterval()
            {
                StateIntervalTime = 0;
            }

            public void InitalizeTaskInterval()
            {
                TaskIntervalTime = 0;
            }
        }

        [SerializeField] EnemyAIAttackData _attackData;
        [SerializeField] EnemyAISystemData _aiData;

        int _taskID;
        bool _onAttack;

        IntervalData _intervalData;
        StateData _currentState;
        TaskData _currentTask;

        public Vector2 MoveDir { get; private set; }

        public void Setup(Transform user)
        {
            _taskID = 0;

            _intervalData = new IntervalData();

            _aiData.SetupUser(user);
            _currentState = _aiData.GetStateData(_aiData.StartStatePath);
        }

        public void Process()
        {
            _intervalData.AddTime();
            
            if (_onAttack)
            {
                MoveDir = _currentTask.Move.Execute().normalized;
            }
            else
            {
                SetNextState();
                OnTask();
            }
        }

        void OnTask()
        {
            _currentTask = GetTaskData();

            IAttack attack = _currentTask.Move as IAttack;

            if (attack != null && !_onAttack)
            {
                OnAttack(attack);
            }

            MoveDir = _currentTask.Move.Execute().normalized;
        }

        void OnAttack(IAttack attack)
        {
            _onAttack = true;

            AttackData data = _attackData.GetAttackData(attack.SendAttackIndex());
            StartCoroutine(AttackProcess(data));
        }

        IEnumerator AttackProcess(AttackData data)
        {
            for (int index = 0; index < data.IsActiveFrame; index++)
            {
                yield return null;
            }
            
            data.Collider.SetColliderActive(true);

            for (int index = 0; index < data.EndActiveFrame; index++)
            {
                yield return null;
            }

            data.Collider.SetColliderActive(false);

            for (int index = 0; index < data.WaitExitFrame; index++)
            {
                yield return null;
            }

            _onAttack = false;
        }

        TaskData GetTaskData()
        {
            if (_taskID >= _currentState.TaskDataList.Count)
            {
                _taskID = 0;
            }

            TaskData data = _currentState.TaskDataList[_taskID];

            if (data.TaskIntervalTime < _intervalData.TaskIntervalTime)
            {
                _intervalData.InitalizeTaskInterval();
                data.Move.Initalize();
                _taskID++;
            }

            return data;
        }

        void SetNextState()
        {
            if (_aiData.StateIntervalTime < _intervalData.StateIntervalTime)
            {
                string nextStatePath = ExitState();
                Debug.Log(nextStatePath);
                if (nextStatePath != _currentState.StatePath)
                {
                    ChangeState(nextStatePath);
                    return;
                }
            }
        }

        string ExitState()
        {
            foreach (ExitData data in _currentState.ExitDataList)
            {
                if (data.ConditionList.All(c => c.Try()))
                {
                    return data.NextStatePath;
                }
            }

            return _currentState.StatePath;
        }

        /// <summary>
        /// 次のステートを取得
        /// </summary>
        /// <param name="nextStatePath">次のステートパス</param>
        void ChangeState(string nextStatePath)
        {
            _taskID = 0;

            _intervalData.InitalizeStateInterval();
            _intervalData.InitalizeTaskInterval();

            _currentState.ExitDataList.ForEach(e => e.ConditionList.ForEach(c => c.Initalize()));
            _currentState.TaskDataList.ForEach(t => t.Move.Initalize());

            _currentState = _aiData.GetStateData(nextStatePath);
        }
    }

}