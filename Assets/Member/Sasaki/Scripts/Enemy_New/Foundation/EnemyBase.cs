using MonoState;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public interface IBehaviourDatable
{
    bool OnAttack { get; set; }
    Vector2 SetMoveDirection { set; }
    RigidOperator Rigid { get; }
}


[RequireComponent(typeof(RigidOperator))]
public abstract class EnemyBase : MonoBehaviour, IFieldObjectDatable, IDamagable, IGameDisposable, IBehaviourDatable
{
    public enum State
    {
        Idle,
        Move,
        Attack,
        KnockBack,
    }

    [SerializeField] int _speed;
    /// <summary>最大HP</summary>
    [SerializeField] int _maxHp;
    [SerializeField] bool _isInstantiateFloat;
    [SerializeField] Slider _slider;
    [SerializeField] DamageText _damageText;
    [SerializeField] EnemyAttackCollider _attackCollider;

    float _xScale = 0;
    Vector2 _beforePosition = Vector2.zero;
    int _id = 0;
    List<StatusEffectBase> _statusEffects = new List<StatusEffectBase>();

    /// <summary>現在HP</summary>
    ReactiveProperty<int> _currentHp = new ReactiveProperty<int>();

    protected int Speed => _speed;
    protected Vector2 MoveDirection { get; private set; }
    protected RigidOperator Rigid { get; private set; }
    protected MonoStateMachine<EnemyBase> MonoState { get; private set; }
    protected EnemyStateData EnemyStateData { get; private set; } = new EnemyStateData();
    public int ID { get => _id; set => _id = value; }
    /// <summary>状態異常の付与</summary>
    public StatusEffectBase SetStatusEffect
    {
        set
        {
            _statusEffects.Add(value);
            //value.EndEvent
            //    .Subscribe(_ => _statusEffects.Remove(value))
            //    .AddTo(this);
            //value.Effect(AddDamage, this);
        }
    }
    public void StatusEffectRemove(StatusEffectBase statusEffect) => _statusEffects.Remove(statusEffect);

    /// <summary>
    /// 生成時に空中に生成することを許容するかどうか
    /// </summary>
    public bool IsInstantiateFloat => _isInstantiateFloat;

    GameObject IFieldObjectDatable.Target => gameObject;

    bool IBehaviourDatable.OnAttack { get; set; } = false;
    Vector2 IBehaviourDatable.SetMoveDirection { set { MoveDirection = value; } }
    RigidOperator IBehaviourDatable.Rigid => Rigid;

    ObjectType IFieldObjectDatable.ObjectType => ObjectType.Enemy;

    public ObjectType ObjectType => ObjectType.Enemy;

    void Awake()
    {
        GameController.Instance.AddFieldObjectDatable(this);
        GameController.Instance.AddGameDisposable(this);
    }

    void Start()
    {
        _xScale = Mathf.Abs(transform.localScale.x);

        Rigid = GetComponent<RigidOperator>();
        Rigid.FreezeRotation = true;

        MonoState = new MonoStateMachine<EnemyBase>(this);
        MonoState
            .AddMonoData(EnemyStateData);

        //Sliderの初期化
        _slider.maxValue = _maxHp;
        _slider.value = _maxHp;
        _currentHp.Value = _maxHp;
        _currentHp.Subscribe(i => _slider.value = i).AddTo(this);

        _beforePosition = transform.position;

        EnemyStateData.AttackCollider = _attackCollider;
        EnemyStateData.IBehaviourDatable = this;

        Setup();
    }

    void Update()
    {
        EnemyStateData.MoveDirection = MoveDirection;

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
    protected virtual void DeadEvent()
    {
        SoundManager.PlayRequest(SoundSystem.SoundType.SEEnemy, "Dead");
        EffectStocker.Instance.LoadEffect("Dead1", transform.position);
        EffectStocker.Instance.LoadEffect("Dead2", transform.position);
        EffectStocker.Instance.LoadEffect("Dead3", transform.position);
    }

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
            scale.x = Mathf.Sign(forward.x) * _xScale;
            transform.localScale = scale;
        }
    }

    public void AddDamage(int damage)
    {
        if (IsDamage(damage))
        {
            SoundManager.PlayRequest(SoundSystem.SoundType.SEEnemy, "Hit");
            DamageText.Init(_damageText, damage.ToString(), transform.position, Color.red);
            _currentHp.Value -= damage;
        }

        if (_currentHp.Value <= 0)
        {
            DeadEvent();
            Destroy(gameObject);
        }
    }

    void IGameDisposable.GameDispose()
    {
        if (this == null) return;

        Destroy(gameObject);
    }
}
