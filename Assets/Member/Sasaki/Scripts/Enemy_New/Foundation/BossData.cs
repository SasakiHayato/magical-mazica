using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossData
{
    [SerializeField] float _farDistance;
    [SerializeField] float _nearDistance;
    [SerializeField] float _updateSpeedRate;
    [SerializeField] float _downSpeedRate;
    [SerializeField] List<BossTaskData> _bossTaskList;

    public float FarDist => _farDistance;
    public float NearDist => _nearDistance;
    public float UpdateSpeed => _downSpeedRate;
    public float DownSpeedRate => _downSpeedRate;

    public int TaskDataLength => _bossTaskList.Count;

    public void TaskSetup(Transform user)
    {
        _bossTaskList.ForEach(t => t.Setup(user));
    }

    public BossTaskData GetTaskData(int id)
    {
        return _bossTaskList[id];
    }
}
