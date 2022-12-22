using UnityEngine;

public class Boss : EnemyBase, IInputEventable
{
    [SerializeField] Vector2 _forceDirection;
    [SerializeField] BossData _bossData;

    bool _isRejectionTask = false;

    BossTaskProcesser _taskProcesser = new BossTaskProcesser();
    Transform _core = null;

    protected override void Setup()
    {
        _bossData.TaskSetup(transform);

        CreateCore();
        MonoState.AddState(State.Idle, new EnemyStateIdle());
        GUIManager.ShowBossHealthBar(MaxHP, HPObservable, this);

        MonoState.IsRun = true;
    }

    void CreateCore()
    {
        _core = new GameObject("Core").transform;
        _core.position = transform.position;
        _core.localScale = Vector2.one / transform.localScale.x;
        _core.SetParent(transform);
    }

    protected override void Execute()
    {
        if (_isRejectionTask) return;
        
        OnMove();
        OnTask();
    }

    void OnMove()
    {
        if (GameController.Instance.Player == null) return;
        
        float speed = Speed;
        float distance = Mathf.Abs(_core.position.x) - Mathf.Abs(GameController.Instance.Player.position.x);
        
        if (Mathf.Abs(distance) > _bossData.FarDist)
        {
            speed *= _bossData.UpdateSpeed;
        }

        if (Mathf.Abs(distance) < _bossData.NearDist)
        {
            speed /= _bossData.DownSpeedRate;
        }

        Rigid.SetMoveDirection = Vector2.right * -1 * speed;
    }

    void OnTask()
    {
        if (!_taskProcesser.OnProcess())
        {
            int id = Random.Range(0, _bossData.TaskDataLength);
            _taskProcesser.SetTaskData(_bossData.GetTaskData(id));
        }
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
            damagable.AddDamage(1);
        }

        if (collision.TryGetComponent(out IDamageForceble forceble))
        {
            forceble.OnFoece(_forceDirection);
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
