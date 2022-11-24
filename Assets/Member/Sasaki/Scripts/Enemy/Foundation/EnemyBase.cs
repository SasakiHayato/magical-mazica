using MonoState;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour, IFieldObjectDatable, IDamagable, IGameDisposable
{
    public enum State
    {
        Idle,
        Move,
        Attack,
    }

    [SerializeField] int _speed;
    [SerializeField] int _hp;
    [SerializeField] bool _isInstantiateFloat;
    
    Vector2 _beforePosition = Vector2.zero;

    Transform _player;
    
    protected int Speed => _speed;
    
    protected Rigidbody2D RB { get; private set; }
    protected MonoStateMachine<EnemyBase> MonoState { get; private set; }
    protected EnemyStateData EnemyStateData { get; private set; } = new EnemyStateData();

    /// <summary>
    /// 生成時に空中に生成することを許容するかどうか
    /// </summary>
    public bool IsInstantiateFloat => _isInstantiateFloat;

    GameObject IFieldObjectDatable.Target => gameObject;
    
    void Awake()
    {
        GameController.Instance.AddFieldObjectDatable(this);
        GameController.Instance.AddGameDisposable(this);
    }

    void Start()
    {
        _player = GameController.Instance.Player;

        RB = GetComponent<Rigidbody2D>();
        RB.freezeRotation = true;
        
        MonoState = new MonoStateMachine<EnemyBase>(this);
        MonoState.AddMonoData(EnemyStateData);

        _beforePosition = transform.position;
        Setup();
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, _player.position);
        EnemyStateData.PlayerDistance = dist;

        Rotate();
        Execute();
    }

    protected abstract void Setup();
    protected abstract void Execute();
    protected abstract bool IsDamage(int damage);

    /// <summary>
    /// スケールをいじって回転を表現
    /// </summary>
    void Rotate()
    {
        Vector2 forward = (transform.position.Collect() - _beforePosition).normalized;
        _beforePosition = transform.position;

        if (Mathf.Abs(forward.x) > 0.01f)
        {
            Vector2 scale = transform.localScale;
            scale.x = Mathf.Sign(forward.x);
            transform.localScale = scale;
        }
    }

    void IDamagable.AddDamage(int damage)
    {
        if (IsDamage(damage))
        {
            SoundManager.PlayRequest(SoundSystem.SoundType.SEEnemy, "Hit");
            _hp -= damage;
        }

        if (_hp <= 0)
        {
            SoundManager.PlayRequest(SoundSystem.SoundType.SEEnemy, "Dead");
            EffectStocker.LoadEffect("Dead", transform.position);
            Destroy(gameObject);
        }
    }

    void IGameDisposable.GameDispose()
    {
        if (this == null) return;

        Destroy(gameObject);
    }
}
