using System.Collections.Generic;
using UnityEngine;

public interface IBossTask
{
    void Setup(Transform user, Boss_NewData data);
    bool OnExecute();
    void Initalize();
}

public interface IBossTaskOnEnableEventable
{
    void OnEnableEvent();
}

public interface IBossTaskOnEndEventable
{
    void OnEndEvent();
}

[System.Serializable]
public class Boss_NewTaskData
{
    [System.Serializable]
    public class Task
    {
        [SerializeReference, SubclassSelector]
        List<IBossTask> _taskList;

        public int TaskCount => _taskList.Count;

        public IBossTask GetTask(int id) => _taskList[id];
        public void Setup(Transform user, Boss_NewData data) => _taskList.ForEach(t => t.Setup(user, data));
        public void Initalize() => _taskList.ForEach(t => t.Initalize());
    }

    [SerializeField] float _onNextMinIntervalTime;
    [SerializeField] float _onNextMaxIntervalTime;
    [SerializeField] List<Task> _taskDataList;

    int _currentTaskID = 0;
    public int ReadCurrentTaskID => _currentTaskID;
    public int ReadTaskDataCount => _taskDataList.Count;

    public void Setup(Transform user, Boss_NewData data)
    {
        _taskDataList.ForEach(t => t.Setup(user, data));
    }

    public void OnNextCurrentID() => _currentTaskID++;
    public void InitalizeCurrentTaskID() => _currentTaskID = 0;

    public float GetOnNextIntervalTime() => Random.Range(_onNextMinIntervalTime, _onNextMaxIntervalTime);

    public Task GetTask(int id) => _taskDataList[id];
}
