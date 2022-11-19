using MonoState;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour, IFieldObjectDatable, IDamagable
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

    void OnDestroy()
    {
        GameController.Instance.RemoveFieldObjectDatable(this);
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
        forward.y = 1;
        _beforePosition = transform.position;

        if (Mathf.Abs(forward.x) > 0.01f)
        {
            forward.x = Mathf.Sign(forward.x) * 1;
            transform.localScale = forward;
        }
    }

    void IDamagable.AddDamage(int damage)
    {
        if (IsDamage(damage))
        {
            _hp -= damage;
        }

        if (_hp <= 0)
        {
            Debug.Log("死んだ");
        }
    }
}
