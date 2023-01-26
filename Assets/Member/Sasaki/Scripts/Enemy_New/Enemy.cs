using UnityEngine;

public class Enemy : EnemyBase, IDamageForceble, IInputEventable
{
    [SerializeField] int _attackIsActiveFrame;
    [SerializeField] int _attackEndActiveFrame;
    [SerializeField] AnimOperator _animOperator;
    [SerializeField] BehaviourTree.BehaviourTreeUser _treeUser;
    [SerializeField] FusionMaterialObject _materialObject;
    [SerializeField] RawMaterialData _materialData;

    System.Action<GameObject> _deadCallback = null;
    public System.Action<GameObject> SetDeadCallback { set { _deadCallback = value; } }

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

        if (GameController.Instance.OnInputEvent)
        {
            gameObject.GetComponent<IInputEventable>().OnEvent();
        }
    }

    protected override void Execute()
    {
        Rigid.SetMoveDirection = MoveDirection * Speed;
    }
    protected override void DeadEvent()
    {
        _deadCallback.Invoke(gameObject);
        RawMaterialDatabase rawMaterial = _materialData.GetMaterialDataRandom();

        Player player = GameController.Instance.Player.GetComponent<Player>();
        FusionMaterialObject.Init(_materialObject, transform.position, rawMaterial, player);

        base.DeadEvent();
    }

    protected override bool IsDamage(int damage)
    {
        EffectStocker.Instance.LoadEffect("Damage", transform.position);
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
