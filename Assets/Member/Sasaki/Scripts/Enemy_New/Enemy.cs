using UnityEngine;

public class Enemy : EnemyBase, IDamageForceble, IInputEventable
{
    [SerializeField] int _attackIsActiveFrame;
    [SerializeField] int _attackEndActiveFrame;
    [SerializeField] AnimOperator _animOperator;
    [SerializeField] BehaviourTree.BehaviourTreeUser _treeUser;


    protected override void Setup()
    {
        MonoState
            .AddState(State.Idle, new EnemyStateIdle())
            .AddState(State.Move, new EnemyStateRun())
            .AddState(State.Attack, new EnemyStateAttack())
            .AddState(State.KnockBack, new EnemyStateKnockBack());

        EnemyStateData.AttackAciveFrame = (_attackIsActiveFrame, _attackEndActiveFrame);
        MonoState
            .AddMonoData(_animOperator)
            .AddMonoData(_treeUser);

        MonoState.IsRun = true;
    }

    protected override void Execute()
    {
        Rigid.SetMoveDirection = MoveDirection * Speed;
    }
    protected override void DeadEvent()
    {
        CreateMap.Instance.DeadEnemy(gameObject);
        base.DeadEvent();
    }

    protected override bool IsDamage(int damage)
    {
        return true;
    }

    void IDamageForceble.OnFoece(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        Rigid.SetImpulse(direction.x, RigidMasterData.ImpulseDirectionType.Horizontal, true);
        Rigid.SetImpulse(direction.y, RigidMasterData.ImpulseDirectionType.Vertical, true);

        MonoState.ChangeState(State.KnockBack);
    }

    void IInputEventable.OnEvent()
    {
        _treeUser.SetRunRequest(false);
        GetComponent<IBehaviourDatable>().SetMoveDirection = Vector2.zero;
    }

    void IInputEventable.DisposeEvent()
    {
        _treeUser.SetRunRequest(true);
    }
}
