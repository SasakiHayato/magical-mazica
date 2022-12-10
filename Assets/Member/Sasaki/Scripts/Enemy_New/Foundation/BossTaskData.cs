using System.Collections.Generic;
using UnityEngine;

public interface IBossAttackTask
{
    void Setup(Transform user, EnemyAttackCollider attackCollider);
    void Execute();
    void Initalize();
}

[System.Serializable]
public class BossTaskData 
{
    [System.Serializable]
    public class Data
    {
        [SerializeField] float _onNextTimer;
        [SerializeField] EnemyAttackCollider _attackCollider;
        [SerializeReference, SubclassSelector] IBossAttackTask _task;

        public float OnNextTime => _onNextTimer;
        public IBossAttackTask OnTask => _task;

        public void Setup(Transform user)
        {
            _task.Setup(user, _attackCollider);
        }
    }

    [SerializeField] List<Data> _taskDataList;

    public void Setup(Transform user)
    {
        _taskDataList.ForEach(t => t.Setup(user));
    }

    public List<Data> GetData()
    {
        return _taskDataList;
    }
}
