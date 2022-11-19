using UnityEngine;
using EnemyAISystem;

public class EnemyAttackEventDead : IEnemyAttackEvent
{
    Transform _user;

    public void Setup(Transform user)
    {
        _user = user;
    }

    public void EnableEvent()
    {
        
    }

    public void ExecuteEvent()
    {
        
    }

    public void EndEvent()
    {
        Object.Destroy(_user.gameObject);
    }
}
