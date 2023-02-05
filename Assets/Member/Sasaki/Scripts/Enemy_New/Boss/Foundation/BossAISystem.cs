using UnityEngine;

public class BossAISystem : MonoBehaviour
{
    [SerializeField] Transform _user;
    [SerializeField] Boss_NewTaskData _taskData;

    Boss_NewTaskData.Task _currentTask = null;

    float _timer = 0;
    float _intervalTime = 0;
    bool _onEndCurrentTask = false;

    public void Setup(Boss_NewData data)
    {
        _taskData.Setup(_user, data);

        SetTask(0);

        _intervalTime = _taskData.GetOnNextIntervalTime();
    }

    public void OnExecuteTask()
    {
        _timer += Time.deltaTime;

        if (_timer > _intervalTime && _onEndCurrentTask) 
        {
            Initalize();
            SetTask();
        } 
        else
        {
            OnTask();
        }
    }

    void OnTask()
    {
        if (_onEndCurrentTask) return;
        
        if (_currentTask.GetTask(_taskData.ReadCurrentTaskID).OnExecute())
        {
            IBossTaskOnEndEventable end = _currentTask.GetTask(_taskData.ReadCurrentTaskID) as IBossTaskOnEndEventable;

            if (end != null)
            {
                end.OnEndEvent();
            }

            _taskData.OnNextCurrentID();
            
            if (_taskData.ReadCurrentTaskID >= _currentTask.TaskCount)
            {
                _onEndCurrentTask = true;
            }
            else
            {
                SetTask(_taskData.ReadCurrentTaskID - 1);

                _onEndCurrentTask = false;
            }
        }
    }

    void Initalize()
    {
        _timer = 0;
        _onEndCurrentTask = false;

        _currentTask.Initalize();
        _taskData.InitalizeCurrentTaskID();
        _intervalTime = _taskData.GetOnNextIntervalTime();
    }

    void SetTask(int id = default)
    {
        if (id == default)
        {
            id = Random.Range(0, _taskData.ReadTaskDataCount);
        }
        
        _currentTask = _taskData.GetTask(id);
        IBossTaskOnEnableEventable eventable = _currentTask.GetTask(_taskData.ReadCurrentTaskID) as IBossTaskOnEnableEventable;
        if (eventable != null)
        {
            eventable.OnEnableEvent();
        }
    }

    public float CollectMoveSpeed(Boss_NewData data, float defaultSpeed)
    {
        float playerPositionX = GameController.Instance.Player.transform.position.x;
        float ditance = Mathf.Abs(data.Distance.Core.position.x - playerPositionX);
        
        if (ditance > data.Distance.Far) return data.MoveSpeed.Fast * defaultSpeed;
        if (ditance < data.Distance.Near) return data.MoveSpeed.Srow * defaultSpeed;
        else return defaultSpeed;
    }
}
