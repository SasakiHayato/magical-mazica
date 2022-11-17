using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using MonoState;
using MonoState.Data;
public class Player : MonoBehaviour, IDamagable,IRetentionData
{
    public enum PlayerState
    {
        Idle,
        Move,
        Run,
        Jump,
        Attack
    }
    [SerializeField] int _maxHP;
    ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    [SerializeField] int _damage = 5;
    [SerializeField] RawMaterialID[] _materialID = { RawMaterialID.Empty, RawMaterialID.Empty };
    [SerializeField] Vector2 _jumpDir;
    Rigidbody2D _rb;
    bool _isGrounded;
    Animator _anim;
    FusionItem _fusionItem;
    Storage _storage;
    FieldTouchOperator _fieldTouchOperator;

    /// <summary>�U����</summary>
    public int Damage { get => _damage; set { } }
    /// <summary>�ő�HP</summary>
    public int MaxHP => _maxHP;
    public Vector2 Direction { get; set; }
    /// <summary>����HP�̍X�V�̒ʒm</summary>
    public System.IObservable<int> CurrentHP => _hp;

    string IRetentionData.Path => nameof(Player);

    MonoStateMachine<Player> _stateMachine = new MonoStateMachine<Player>();
    
    private void Start()
    {
        _stateMachine.Initalize(this);
        _stateMachine.SetData(this);
        _stateMachine
            .AddState(new PlayerIdle(), PlayerState.Idle)
            .AddState(new PlayerRun(), PlayerState.Run)
            .AddState(new PlayerJump(), PlayerState.Jump)
            .AddState(new PlayerAttack(),PlayerState.Attack);
        _stateMachine.SetRunRequest(PlayerState.Idle);

        TryGetComponent(out _rb);
        TryGetComponent(out _anim);
        _storage = GetComponentInChildren<Storage>();
        _fieldTouchOperator = GetComponentInChildren<FieldTouchOperator>();
        _fusionItem = FindObjectOfType<FusionItem>();
        _hp.Value = _maxHP;
    }

    /// <summary>
    /// �U��
    /// </summary>
    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }
    /// <summary>
    /// �������U��
    /// </summary>
    public void Fire()
    {
        _fusionItem.Attack(transform.position);
    }

    /// <summary>
    /// �W�����v
    /// </summary>
    public void Jump()
    {
        if (_fieldTouchOperator.IsTouch(FieldTouchOperator.TouchType.Ground))
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    //void WallJump()
    //{
    //    if (transform.localScale.x == 1)
    //    {
    //        print("aaa");
    //        _jumpDir = new Vector2(Vector2.left.x, Vector2.up.y);
    //        _rb.AddForce(_jumpDir * _jumpPower, ForceMode2D.Impulse);
    //    }
    //    else
    //    {
    //        print("bbb");
    //        _jumpDir = new Vector2(Vector2.right.x, Vector2.up.y);
    //        _rb.AddForce(_jumpDir * _jumpPower,ForceMode2D.Impulse);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            //WallJump();
        }
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    /// <param name="dir"></param>
    private void PlayerMove(Vector2 dir)
    {
        //float h = Input.GetAxisRaw("Horizontal") * _speed;
        Vector2 velocity = new Vector2(dir.x * _speed, _rb.velocity.y);
        _rb.velocity = velocity;
        if (dir.x != 0)
        {
            if (dir.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    /// <summary>
    /// �}�e���A����ID���Z�b�g
    /// </summary>
    /// <param name="id"></param>
    public void SetMaterialID(RawMaterialID id)
    {
        //��������Ȃ��ꍇ�I���o���Ȃ��悤�ɂ���
        _materialID[1] = _materialID[0];
        _materialID[0] = id;
        print(_materialID[0]);
        print(_materialID[1]);
    }

    /// <summary>
    /// �I�����ꂽ�f�ނ�ID���󂯎��B������
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
                print("�ł���");
            }
            else
            {
                print("�f�ނ�����܂���");
            }
        }
        else if (count[_materialID[0]] >= 1)
        {
            if (count[_materialID[1]] >= 1)
            {
                _fusionItem.Fusion(_materialID[0], _materialID[1]);
                count[_materialID[0]]--;
                count[_materialID[1]]--;
                print("�ł���");
            }
            else
            {
                print("�f�ނ�����܂���");
            }
        }
        else
        {
            print("�f�ނ�����܂���");
        }
    }

    private void FixedUpdate()
    {
        PlayerMove(Direction);
    }

    public void AddDamage(int damage)
    {
        _hp.Value -= damage;
    }

    Object IRetentionData.RetentionData()
    {
        return this;
    }
}