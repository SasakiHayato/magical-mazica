using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using MonoState;
using MonoState.Data;
public class Player : MonoBehaviour, IDamagable, IFieldObjectDatable, IMonoDatableUni<Player>
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Attack,
        WallJump,
        Float
    }
    [SerializeField] int _maxHP;
    ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    [SerializeField] float _durationTime;
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _wallJumpPower;
    [SerializeField] int _damage = 5;
    RawMaterialID[] _materialID = { RawMaterialID.Empty, RawMaterialID.Empty };
    bool _isJumped;
    bool _isWallJumped;
    bool _isAttacked;
    Rigidbody2D _rb;
    Animator _anim;
    FusionItem _fusionItem;
    Storage _storage;
    FieldTouchOperator _fieldTouchOperator;

    /// <summary>攻撃力</summary>
    public int Damage { get => _damage; private set => _damage = value; }
    /// <summary>最大HP</summary>
    public int MaxHP => _maxHP;
    /// <summary>移動速度</summary>
    public float Speed { get => _speed; private set => _speed = value; }
    /// <summary>ジャンプ力</summary>
    public float JumpPower { get => _jumpPower; private set => _jumpPower = value; }
    public float WallJumpPower { get => _wallJumpPower; private set => _wallJumpPower = value; }
    public bool IsJumped { get => _isJumped; set => _isJumped = value; }
    public bool IsWallJumped { get => _isWallJumped; set => _isWallJumped = value; }
    public bool IsAttacked { get => _isAttacked; set => _isAttacked = value; }
    public Rigidbody2D Rigidbody { get => _rb; private set => _rb = value; }
    public Vector2 Direction { get; set; }
    /// <summary>現在HPの更新の通知</summary>
    public System.IObservable<int> CurrentHP => _hp;

    public FieldTouchOperator FieldTouchOperator { get => _fieldTouchOperator; private set => _fieldTouchOperator = value; }
    public GameObject Target => gameObject;
    public Player GetData => this;
    string IMonoDatable.Path => nameof(Player);

    MonoStateMachine<Player> _stateMachine;

    private void Awake()
    {
        GameController.Instance.AddFieldObjectDatable(this);
    }
    private void Start()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _anim);
        _storage = GetComponentInChildren<Storage>();
        _fieldTouchOperator = GetComponentInChildren<FieldTouchOperator>();
        _fusionItem = FindObjectOfType<FusionItem>();
        _hp.Value = _maxHP;

        _stateMachine = new MonoStateMachine<Player>(this, _durationTime);
        _stateMachine
            .AddState(PlayerState.Idle, new PlayerIdle())
            .AddState(PlayerState.Run, new PlayerRun())
            .AddState(PlayerState.Jump, new PlayerJump())
            .AddState(PlayerState.Attack, new PlayerAttack())
            .AddState(PlayerState.WallJump, new PlayerWallJump())
            .AddState(PlayerState.Float, new PlayerFloat());

        _stateMachine.AddMonoData(this);
        _stateMachine.IsRun = true;
    }

    void OnDestroy()
    {
        GameController.Instance.RemoveFieldObjectDatable(this);
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }
    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    public void Fire()
    {
        _fusionItem.Attack(transform.position);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    public void Jump()
    {
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground, true))
        {
            _isJumped = true;
        }
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            _isWallJumped = true;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="dir"></param>
    private void PlayerMove(Vector2 dir)
    {
        //_isRun = true;
        //float h = Input.GetAxisRaw("Horizontal") * _speed;
        //Vector2 velocity = new Vector2(dir.x * _speed, _rb.velocity.y);
        //_rb.velocity = velocity;
        //if (dir.x != 0)
        //{
        //    if (dir.x < 0)
        //    {
        //        transform.localScale = new Vector3(-1, 1, 1);
        //    }
        //    else
        //    {
        //        transform.localScale = new Vector3(1, 1, 1);
        //    }
        //}
    }

    /// <summary>
    /// マテリアルのIDをセット
    /// </summary>
    /// <param name="id"></param>
    public void SetMaterialID(RawMaterialID id)
    {
        //個数が足りない場合選択出来ないようにする
        _materialID[1] = _materialID[0];
        _materialID[0] = id;
        print(_materialID[0]);
        print(_materialID[1]);
    }

    /// <summary>
    /// 選択された素材のIDを受け取り錬成する
    /// </summary>
    public void Fusion()
    {
        var count = _storage.MaterialCount;
        if (_materialID[0] == _materialID[1])
        {
            if (count[_materialID[0]] >= 2)
            {
                _fusionItem.Fusion(_materialID[0], _materialID[1]);
                count[_materialID[0]] -= 2;
                print("できた");
            }
            else
            {
                print("素材が足りません");
            }
        }
        else if (count[_materialID[0]] >= 1)
        {
            if (count[_materialID[1]] >= 1)
            {
                _fusionItem.Fusion(_materialID[0], _materialID[1]);
                count[_materialID[0]]--;
                count[_materialID[1]]--;
                print("できた");
            }
            else
            {
                print("素材が足りません");
            }
        }
        else
        {
            print("素材が足りません");
        }
    }

    private void FixedUpdate()
    {
        //PlayerMove(Direction);
        if (Direction.x != 0)
        {
            if (Direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        Debug.Log(_stateMachine.CurrentKey);
    }
    private void Update()
    {

    }
    public void AddDamage(int damage)
    {
        _hp.Value -= damage;
    }
}
