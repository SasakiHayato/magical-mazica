using UnityEngine;
using MonoState.Data;

[System.Serializable]
public class PlayerStateData : IMonoDatableSystem<PlayerStateData>
{
    public enum AttackType
    {
        Default,
        Mazic,
    }

    PlayerStateData IMonoDatableSystem<PlayerStateData>.GetData => this;
    string IMonoDatable.Path => nameof(PlayerStateData);

    [System.Serializable]
    public class JumpData
    {
        [SerializeField] int _defaultJumpCount = 2;
        [SerializeField] float[] _power;
        [SerializeField] Vector2 _wallPower = new Vector2(20, 25);

        int _currentJumpID = 0;

        public float Power => _power[_currentJumpID];
        public Vector2 WallPower => _wallPower;

        public int CurrentJumpCount { get; private set; }

        public void SetNextID()
        {
            if (_currentJumpID >= _power.Length)
            {
                _currentJumpID = 0;
                return;
            }

            _currentJumpID++;
        }

        public void CallbackJumpCount()
        {
            CurrentJumpCount--;
        }

        public void Initalize()
        {
            CurrentJumpCount = _defaultJumpCount - 1;
            _currentJumpID = 0;
        }
    }

    public class StatusData
    {
        public float Speed { get; private set; } = 0;
        public int HP { get; private set; }

        public void Set(int hp, float speed)
        {
            Speed = speed;
            HP = hp;
        }
    }

    [SerializeField] JumpData _jumpData;
    [SerializeField] RigidOperator _rigidOperator;
    [SerializeField] GameObject _attackCollider;
    [SerializeField] float _dodgeTime;
    [SerializeField] GameObject _afterImage;

    Vector2 _moveDirection = Vector2.zero;
    AttackType _attackType = AttackType.Default;

    public JumpData Jump => _jumpData;
    public RigidOperator Rigid => _rigidOperator;
    public StatusData Status { get; private set; } = new StatusData();
    public GameObject AttackCollider { get => _attackCollider; set => _attackCollider = value; }
    public GameObject AfterImage { get => _afterImage; set => _afterImage = value; }
    public Vector2 ReadMoveDirection => _moveDirection.normalized;
    public Vector2 SetMoveDirection { set { _moveDirection = value; } }
    public AttackType ReadAttackType => _attackType;
    public AttackType SetAttckType { set { _attackType = value; } }
    public float DodgeTime { get => _dodgeTime; private set => _dodgeTime = value; }
}
