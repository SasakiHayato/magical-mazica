using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTaskProcesser
{
    bool _onTask = false;
    int _dataIndex = 0;
    float _taskTimer;

    BossTaskData _currentTask = null;
    BossTaskData.Data _currentData = null;

    public void SetTaskData(BossTaskData task)
    {
        Initalize();

        _currentTask = task;
        _currentData = task.GetData()[0];
    }

    public bool OnProcess()
    {
        if (_currentTask == null || _dataIndex >= _currentTask.GetData().Count) return false;

        if (!_onTask)
        {
            _onTask = true;
            _currentData.OnTask.Execute();
        }

        _taskTimer += Time.deltaTime;
        if (_taskTimer > _currentData.OnNextTime)
        {
            _currentData.OnTask.Initalize();

            _taskTimer = 0;
            _dataIndex++;
            _onTask = false;

            try
            {
                _currentData = _currentTask.GetData()[_dataIndex];
            }
            catch
            {
                return false;
            }

            
        }

        return true;
    }

    void Initalize()
    {
        _dataIndex = 0;
        _taskTimer = 0;
        _onTask = false;
    }
}
