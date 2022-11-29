using MonoState;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

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
    /// <summary>�ő�HP</summary>
    [SerializeField] int _maxHp;
    [SerializeField] bool _isInstantiateFloat;
    [SerializeField] Slider _slider;
    [SerializeField] DamageText _damageText;
    
    Vector2 _beforePosition = Vector2.zero;
    /// <summary>����HP</summary>
    ReactiveProperty<int> _currentHp = new ReactiveProperty<int>();
    Transform _player;
    
    protected int Speed => _speed;
    protected Rigidbody2D RB { get; private set; }
    protected MonoStateMachine<EnemyBase> MonoState { get; private set; }
    protected EnemyStateData EnemyStateData { get; private set; } = new EnemyStateData();

    /// <summary>
    /// �������ɋ󒆂ɐ������邱�Ƃ����e���邩�ǂ���
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

        //Slider�̏�����
        _slider.maxValue = _maxHp;
        _slider.value = _maxHp;
        _currentHp.Subscribe(i => _slider.value = i).AddTo(this);

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
    /// �X�P�[�����������ĉ�]��\��
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
            DamageText.Init(_damageText, damage.ToString(), transform.position, Color.red);
            _currentHp.Value -= damage;
        }

        if (_currentHp.Value <= 0)
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
