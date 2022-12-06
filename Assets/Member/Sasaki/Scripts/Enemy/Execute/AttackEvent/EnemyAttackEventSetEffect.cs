using UnityEngine;
using EnemyAISystem;

public class EnemyAttackEventSetEffect : IEnemyAttackEvent
{
    [SerializeField] string _effectPath;
    Transform _user;

    void IEnemyAttackEvent.Setup(Transform user)
    {
        _user = user;
    }

    void IEnemyAttackEvent.EnableEvent()
    {
        EffectStocker.Instance.LoadEffect(_effectPath, _user.position);
    }

    void IEnemyAttackEvent.EndEvent()
    {
        //throw new System.NotImplementedException();
    }

    void IEnemyAttackEvent.ExecuteEvent()
    {
        //throw new System.NotImplementedException();
    }
}
