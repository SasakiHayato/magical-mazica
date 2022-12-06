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
        Float,
        IsStick
    }
    [SerializeField] int _maxHP;
    [SerializeField] float _speed;
    [SerializeField] int _damage = 5;
    [SerializeField] PlayerStateData _playerStateData;
    
    ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    //選択したMaterialのIDをセットする変数
    ReactiveCollection<RawMaterialID> _setMaterial = new ReactiveCollection<RawMaterialID> { RawMaterialID.Empty, RawMaterialID.Empty };
    
    bool _isAttacked;
    
    Animator _anim;
    FusionItem _fusionItem;
    Storage _storage;
    FieldTouchOperator _fieldTouchOperator;

    /// <summary>攻撃力</summary>
    public int Damage { get => _damage; private set => _damage = value; }
    /// <summary>最大HP</summary>
    public int MaxHP => _maxHP;
    
    public bool IsAttacked { get => _isAttacked; set => _isAttacked = value; }
    
    public Storage Storage { get => _storage; private set => _storage = value; }
    
    /// <summary>現在HPの更新の通知</summary>
    public System.IObservable<int> CurrentHP => _hp;
    public System.IObservable<CollectionReplaceEvent<RawMaterialID>> SelectMaterial => _setMaterial.ObserveReplace();
    public FieldTouchOperator FieldTouchOperator { get => _fieldTouchOperator; private set => _fieldTouchOperator = value; }
    
    Player IMonoDatableUni<Player>.GetData => this;
    string IMonoDatable.Path => nameof(Player);
    GameObject IFieldObjectDatable.Target => gameObject;

    MonoStateMachine<Player> _stateMachine;

    private void Awake()
    {
        GameController.Instance.Player = transform;
        GameController.Instance.AddFieldObjectDatable(this);
    }
    private void Start()
    {
        TryGetComponent(out _anim);
        _storage = GetComponentInChildren<Storage>();
        _fieldTouchOperator = GetComponentInChildren<FieldTouchOperator>();
        _fusionItem = FindObjectOfType<FusionItem>();
        _hp.Value = _maxHP;

        _playerStateData.Jump.InitalizeJumpCount();
        _playerStateData.Status.Set(_maxHP, _speed);
        SetupState();
    }

    void SetupState()
    {
        _stateMachine = new MonoStateMachine<Player>(this);
        _stateMachine
            .AddState(PlayerState.Idle, new PlayerIdle())
            .AddState(PlayerState.Run, new PlayerRun())
            .AddState(PlayerState.Jump, new PlayerJump())
            .AddState(PlayerState.Attack, new PlayerAttack())
            .AddState(PlayerState.WallJump, new PlayerWallJump())
            .AddState(PlayerState.Float, new PlayerFloat())
            .AddState(PlayerState.IsStick, new PlayerIsStick());

        _stateMachine
            .AddMonoData(this)
            .AddMonoData(_playerStateData);

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

    public void SetMoveDirection(Vector2 direction)
    {
        _playerStateData.SetMoveDirection = direction;
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    public void Jump()
    {
        if (_playerStateData.Jump.CurrentJumpCount >= 0)
        {
            _stateMachine.ChangeState(PlayerState.Jump);
        }
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            _stateMachine.ChangeState(PlayerState.WallJump);
            //_playerStateData.Jump.SetWallJumpedFrag = true;
        }
    }

    /// <summary>
    /// マテリアルのIDをセット
    /// </summary>
    /// <param name="id"></param>
    public void SetMaterialID(RawMaterialID id)
    {
        //個数が足りない場合選択出来ないようにする
        _setMaterial[1] = _setMaterial[0];
        _setMaterial[0] = id;
        print(_setMaterial[0]);
        print(_setMaterial[1]);
    }

    /// <summary>
    /// 選択された素材のIDを受け取り錬成する
    /// </summary>
    public void Fusion()
    {
        if (_setMaterial[0] == _setMaterial[1])
        {
            if (_storage.GetCount(_setMaterial[0]) >= 2)
            {
                _fusionItem.Fusion(_setMaterial[0], _setMaterial[1]);
                _storage.AddMaterial(_setMaterial[0], -2);
                print("できた");
            }
            else
            {
                print("素材が足りません");
            }
        }
        else if (_storage.GetCount(_setMaterial[0]) >= 1)
        {
            if (_storage.GetCount(_setMaterial[1]) >= 1)
            {
                _fusionItem.Fusion(_setMaterial[0], _setMaterial[1]);
                _storage.AddMaterial(_setMaterial[0], -1);
                _storage.AddMaterial(_setMaterial[1], -1);
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

    private void Update()
    {
        Debug.Log(_stateMachine.CurrentKey);
    }

    private void FixedUpdate()
    {
        if (_playerStateData.ReadMoveDirection.x != 0)
        {
            if (_playerStateData.ReadMoveDirection.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    void IDamagable.AddDamage(int damage)
    {
        _hp.Value -= damage;
    }
}
