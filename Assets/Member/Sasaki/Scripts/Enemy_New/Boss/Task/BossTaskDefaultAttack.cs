using UnityEngine;

public class BossTaskDefaultAttack : IBossTask, IBossTaskOnEnableEventable, IBossTaskOnEndEventable
{
    [SerializeField] EnemyAttackCollider _enemyAttackCollider;
    [SerializeField] int _activeFrame;
    [SerializeField] int _disActiveFrame;

    Boss_NewData _data;

    void IBossTask.Setup(Transform user, Boss_NewData data)
    {
        _data = data;
        _enemyAttackCollider.SetColliderActive(false);
    }

    void IBossTaskOnEnableEventable.OnEnableEvent()
    {
        _data.SetOnAttack = true;
        _data.SetAttackFrame(_activeFrame, _disActiveFrame);
        _data.SetCollider = _enemyAttackCollider;
    }

    bool IBossTask.OnExecute()
    {
        return true;
    }

    void IBossTaskOnEndEventable.OnEndEvent()
    {
        _data.SetOnAttack = false;
    }

    void IBossTask.Initalize()
    {

    }
}
