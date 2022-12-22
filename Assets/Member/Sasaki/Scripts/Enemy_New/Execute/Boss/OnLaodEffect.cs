using UnityEngine;

public class OnLaodEffect : IBossAttackTask
{
    enum PositionType
    {
        Boss,
        AttackCollider,
    }

    [SerializeField] string _effectPath;
    [SerializeField] PositionType _positionType;

    Transform _user;

    void IBossAttackTask.Setup(Transform user, EnemyAttackCollider attackCollider)
    {
        switch (_positionType)
        {
            case PositionType.Boss: _user = user;
                break;
            case PositionType.AttackCollider: _user = attackCollider.transform;
                break;
        }
    }

    void IBossAttackTask.Execute()
    {
        EffectStocker.Instance.LoadEffect(_effectPath, _user);
    }

    void IBossAttackTask.Initalize()
    {
        
    }
}
