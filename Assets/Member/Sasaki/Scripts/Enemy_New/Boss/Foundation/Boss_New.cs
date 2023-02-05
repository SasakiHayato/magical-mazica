using UnityEngine;

public class Boss_New : EnemyBase, IInputEventable
{
    [SerializeField] BossAISystem _bossAISystem;
    [SerializeField] Boss_NewData _bossData;
    [SerializeField] AnimOperator _animOperator;

    bool _isRejectionTask = false;

    protected override void Setup()
    {
        _bossAISystem.Setup(_bossData);

        GUIManager.ShowBossHealthBar(MaxHP, HPObservable, this);

        SetupMonoState();
    }

    void SetupMonoState()
    {
        MonoState
            .AddState(State.Move, new BossEnemyStateMove())
            .AddState(State.Attack, new BossEnemyStateAttack());

        MonoState
            .AddMonoData(_bossData)
            .AddMonoData(_animOperator);

        MonoState.IsRun = true;
    }

    protected override void Execute()
    {
        if (_isRejectionTask) return;
        //Debug.Log(MonoState.CurrentKey);
        OnMove();
        _bossAISystem.OnExecuteTask();
    }

    void OnMove()
    {
        if (GameController.Instance.Player == null) return;

        Rigid.SetMoveDirection = _bossAISystem.CollectMoveSpeed(_bossData, Speed) * -1 * Vector2.right;
    }

    protected override bool IsDamage(int damage)
    {
        return true;
    }

    protected override void DeadEvent()
    {
        base.DeadEvent();
        SceneViewer.SceneLoad(SceneViewer.SceneType.Title);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision.TryGetComponent(out IDamagable damagable))
        {
            damagable.AddDamage(_bossData.Touch.Damage);
        }

        if (collision.TryGetComponent(out IDamageForceble forceble))
        {
            forceble.OnFoece(_bossData.Touch.ForceDirection);
        }
    }

    void IInputEventable.OnEvent()
    {
        _isRejectionTask = true;
    }

    void IInputEventable.DisposeEvent()
    {
        _isRejectionTask = false;
    }
}
