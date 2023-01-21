using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using MonoState;
using MonoState.Data;
using static SoundSystem.SoundType;
using DG.Tweening;

public class Player : MonoBehaviour, IDamagable, IFieldObjectDatable, IMonoDatableUni<Player>, IDamageForceble, IInputEventable
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Attack,
        WallJump,
        Float,
        IsStick,
        KnockBack,
        Dodge
    }
    [SerializeField] bool _isDebug;
    [SerializeField] int _maxHP;
    [SerializeField] float _speed;
    [SerializeField] int _damage = 5;
    //無敵時間
    [SerializeField] float _invincibleTime;
    [SerializeField] AnimOperator _animOperator;
    [SerializeField] PlayerStateData _playerStateData;
    [SerializeField] Storage _storage;
    [SerializeField] FieldTouchOperator _fieldTouchOperator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    //選択したMaterialのIDをセットする変数
    //ReactiveCollection<RawMaterialID> _setMaterial = new ReactiveCollection<RawMaterialID> { RawMaterialID.Empty, RawMaterialID.Empty };
    List<RawMaterialID> _setMaterial = new List<RawMaterialID>() { RawMaterialID.Empty, RawMaterialID.Empty };
    Subject<List<RawMaterialID>> _selectMaterial = new Subject<List<RawMaterialID>>();

    FusionItem _fusionItem;
    Tween _tween;
    bool _isHit;
    bool _isAlphaMax;
    float _timer;
    /// <summary>攻撃力</summary>
    public int Damage { get => _damage; private set => _damage = value; }
    /// <summary>最大HP</summary>
    public int MaxHP => _maxHP;
    public bool IsHit { get => _isHit; set => _isHit = value; }
    public Storage Storage { get => _storage; private set => _storage = value; }
    /// <summary>現在HPの更新の通知</summary>
    public System.IObservable<int> CurrentHP => _hp;
    /// <summary>素材選択状態の通知</summary>
    public System.IObservable<List<RawMaterialID>> SelectMaterial => _selectMaterial;
    public FieldTouchOperator FieldTouchOperator { get => _fieldTouchOperator; private set => _fieldTouchOperator = value; }

    Player IMonoDatableUni<Player>.GetData => this;
    string IMonoDatable.Path => nameof(Player);
    GameObject IFieldObjectDatable.Target => gameObject;

    ObjectType IFieldObjectDatable.ObjectType => ObjectType.Player;

    public ObjectType ObjectType => ObjectType.Player;

    MonoStateMachine<Player> _stateMachine;

    private void Awake()
    {
        GameController.Instance.SetPlayer = transform;
        GameController.Instance.AddFieldObjectDatable(this);
    }
    private void Start()
    {
        _fusionItem = GetComponentInChildren<FusionItem>();
        _hp.Value = _maxHP;

        _playerStateData.Jump.Initalize();
        _playerStateData.Status.Set(_maxHP, _speed);
        _playerStateData.AttackCollider.SetActive(false);

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
            .AddState(PlayerState.IsStick, new PlayerIsStick())
            .AddState(PlayerState.KnockBack, new PlayerKnockBack())
            .AddState(PlayerState.Dodge, new PlayerDodge());

        _stateMachine
            .AddMonoData(this)
            .AddMonoData(_animOperator)
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
        _playerStateData.SetAttckType = PlayerStateData.AttackType.Default;
        _stateMachine.ChangeState(PlayerState.Attack);
    }
    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    public void Fire()
    {
        try
        {
            if (Fusion())
            {
                _playerStateData.SetAttckType = PlayerStateData.AttackType.Mazic;
                _stateMachine.ChangeState(PlayerState.Attack);
                _fusionItem.Attack(new Vector2(transform.localScale.x, 0));
            }
            else
            {
                SoundManager.PlayRequest(SoundSystem.SoundType.SEPlayer , "Miss");
            }
        }
        catch
        {
            Debug.Log("発射拒否");
        }
        
    }

    public void SetMoveDirection(Vector2 direction)
    {
        _playerStateData.SetMoveDirection = direction;
    }
    public void Dodge()
    {
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground,true))
        {
            _stateMachine.ChangeState(PlayerState.Dodge);
        }
    }
    /// <summary>
    /// ジャンプ
    /// </summary>
    public void Jump()
    {
        if (_playerStateData.Jump.CurrentJumpCount >= 0)
        {
            _stateMachine.ChangeState(PlayerState.Jump, true);
            _playerStateData.Jump.SetNextID();
        }
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Wall, true))
        {
            _stateMachine.ChangeState(PlayerState.WallJump);
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
        _selectMaterial.OnNext(_setMaterial);
    }

    /// <summary>
    /// 選択された素材のIDを受け取り錬成する
    /// </summary>
    public bool Fusion()
    {
        if (_setMaterial[0] == _setMaterial[1])
        {
            if (_storage.GetCount(_setMaterial[0]) >= 2)
            {
                _fusionItem.Fusion(_setMaterial[0], _setMaterial[1]);
                _storage.AddMaterial(_setMaterial[0], -2);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (_storage.GetCount(_setMaterial[0]) >= 1)
        {
            if (_storage.GetCount(_setMaterial[1]) >= 1)
            {
                _fusionItem.Fusion(_setMaterial[0], _setMaterial[1]);
                _storage.AddMaterial(_setMaterial[0], -1);
                _storage.AddMaterial(_setMaterial[1], -1);
                _setMaterial.ForEach(m => m = RawMaterialID.Empty);
                _selectMaterial.OnNext(_setMaterial);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        //無敵
        if (_isHit)
        {
            _timer += Time.deltaTime;
            if (_timer > _invincibleTime)
            {
                _isHit = false;
                _timer = 0;
            }
        }
    }
    void Blink()
    {
        if (_tween != null)
        {
            _tween.Kill();
        }
        _tween = DOTween
            .To(value => { }, 0, 1, 0.2f)
            .OnStepComplete(() => DoBlink(!_isAlphaMax))
            .SetLoops(3 * 2, LoopType.Restart)
            .OnComplete(() => DoBlink(false));
        DoBlink(false);
    }
    /// <summary>
    /// 点滅する時に呼び出されます
    /// </summary>
    private void DoBlink(bool isBlink)
    {
        _isAlphaMax = isBlink;

        var color = _spriteRenderer.color;
        color.a = isBlink ? 0.5f : 1;
        _spriteRenderer.color = color;
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
        if (_isHit)
        {
            return;
        }
        Blink();
        _isHit = true;
        _hp.Value -= damage;

        if (_hp.Value <= 0 && !_isDebug)
        {
            SoundManager.PlayRequest(SEPlayer, "Dead");

            Destroy(gameObject);
            SceneViewer.SceneLoad(SceneViewer.SceneType.Title);
        }
        else
        {
            SoundManager.PlayRequestRandom(SEPlayer, "Damage");
            EffectStocker.Instance.LoadEffect("PlayerInvincible", transform);
            EffectStocker.Instance.LoadEffect("Damage", transform.position);
        }
    }

    void IDamageForceble.OnFoece(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        _playerStateData.Rigid.SetImpulse(direction.x, RigidMasterData.ImpulseDirectionType.Horizontal, true);
        _playerStateData.Rigid.SetImpulse(direction.y, RigidMasterData.ImpulseDirectionType.Vertical, true);

        _stateMachine.ChangeState(PlayerState.KnockBack);
    }

    void IInputEventable.OnEvent()
    {
        _playerStateData.SetMoveDirection = Vector2.zero;
    }

    void IInputEventable.DisposeEvent()
    {

    }
}
