using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������̊Ǘ��N���X
/// </summary>
public partial class RigidOperator : MonoBehaviour
{
    [SerializeField] bool _useGravity = true;
    [SerializeField] bool _freezeRotation = true;
    [SerializeField] bool _useInertia = false;
    [SerializeField] float _mass = 1;
    [SerializeField] float _maxAcceleration = 5;
    [SerializeField] FieldTouchOperator _fieldTouch;

    float _timer = 0;
    float _gravityScaleRate = 1;
    Vector2 _moveDirection = Vector2.zero;
    
    List<ImpulseData> _impulseDataList = new List<ImpulseData>();
    Rigidbody2D _rb = null;

    float Gravity => RigidMasterData.Gravity * _timer;

    /// <summary>
    /// �d�͂��g�p
    /// </summary>
    public bool UseGravity { get => _useGravity; set { _useGravity = value; } }
    /// <summary>
    /// RigidBody2D.freezeRotation�ɑ΂��鑀��
    /// </summary>
    public bool FreezeRotation { get => _rb.freezeRotation; set { _rb.freezeRotation = value; } }
    /// <summary>
    /// �������g�p
    /// </summary>
    public bool UseInertia { get => _useInertia; set { _useInertia = value; } }
    /// <summary>
    /// RigidBody2D.mass�ɑ΂��鑀��
    /// </summary>
    public float Mass { get => _rb.mass; set { _rb.mass = value; } }
    /// <summary>
    /// �ő�d�͉����x
    /// </summary>
    public float MaxAcceleration { get => _maxAcceleration; set { _maxAcceleration = value; } }
    /// <summary>
    /// Velocity�ɑ΂�������̐ݒ�
    /// </summary>
    public Vector2 SetMoveDirection { set { _moveDirection = value; } }
    /// <summary>
    /// RigidBody2D.velocity�̓ǂݎ��
    /// </summary>
    public Vector2 ReadVelocity => _rb.velocity;
    
    void Awake()
    {
        // Note. ���L�A�g�p�R���|�[�l���g�̕ۏ�
        _rb = TryGetComponent(out _rb) ? _rb : gameObject.AddComponent<Rigidbody2D>();
        
        if (_fieldTouch == null)
        {
            _fieldTouch = TryGetComponent(out _fieldTouch) ? _fieldTouch : GetComponentInChildren<FieldTouchOperator>();
        }
    }

    void Start()
    {
        _rb.hideFlags = HideFlags.HideInInspector;
        _rb.gravityScale = 0;
        _rb.freezeRotation = _freezeRotation;
        _rb.mass = _mass;

        _impulseDataList.Add(new ImpulseData(RigidMasterData.ImpulseDirectionType.Horizontal));
        _impulseDataList.Add(new ImpulseData(RigidMasterData.ImpulseDirectionType.Vertical));
    }

    void FixedUpdate()
    {
        Acceleration();
        
        float horizontal = Horizontal(_moveDirection.x, _impulseDataList[0]);
        float vertical = Vertical(_moveDirection.y, _impulseDataList[1]);

        _rb.velocity = new Vector2(horizontal, vertical);
    }

    float Horizontal(float moveValue, ImpulseData data)
    {
        float value;

        if (_useInertia)
        {
            float inertia = data.OnInertia(RigidMasterData.InertiaRate);
            value = data.IsMoveLock ? inertia : moveValue + inertia;
        }
        else
        {
            value = data.IsMoveLock ? data.GetValue() : moveValue + data.GetValue(); ;
        }
        
        return value;
    }

    float Vertical(float moveValue, ImpulseData data)
    {
        if (data.IsMoveLock)
        {
            _timer = 0;
            return data.GetValue();
        }
        else
        {
            float gravity = Gravity * RigidMasterData.GravityScale * _gravityScaleRate;
            moveValue = _useGravity ? gravity : moveValue;

            return moveValue + data.GetValue();
        }
    }

    void Acceleration()
    {
        if (!_useGravity) return;

        bool isGround = _fieldTouch.IsTouch(FieldTouchOperator.TouchType.Ground, true);
        _timer = isGround ? 0.05f : Time.fixedDeltaTime + _timer;

        if (_timer > _maxAcceleration)
        {
            _timer = _maxAcceleration;
        }
    }

    /// <summary>
    /// �Ռ��̐ݒ�BRigidBody2D.AddForce(); ForceMode2D.Impulse�̑�p
    /// </summary>
    /// <param name="value">����</param>
    /// <param name="directionType">����</param>
    /// <param name="isMoveLock">�Ռ����󂯂Ă���ԍs���𖳎����邩�ǂ���</param>
    public void SetImpulse(float value, RigidMasterData.ImpulseDirectionType directionType, bool isMoveLock = false)
    {
        ImpulseData data = _impulseDataList.Find(d => d.DirectionType == directionType);
        data.Setup(value, isMoveLock);
    }

    /// <summary>
    /// �d�͉����x�̑�����𒲐�
    /// </summary>
    /// <param name="rate">����</param>
    public void SetGravityRate(float rate)
    {
        _gravityScaleRate = rate;
    }

    /// <summary>
    /// �d�͉����x�̏�����
    /// </summary>
    public void InitalizeGravity()
    {
        _timer = 0;
    }

    public void ResetImpalse()
    {
        _impulseDataList.ForEach(i => i.Initalize());
    }
}
