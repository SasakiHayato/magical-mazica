using UnityEngine;
using MonoState.Data;

[System.Serializable]
public class PlayerStateData : IMonoDatableSystem<PlayerStateData>
{
    PlayerStateData IMonoDatableSystem<PlayerStateData>.GetData => this;
    string IMonoDatable.Path => nameof(PlayerStateData);

    [System.Serializable]
    public class JumpData
    {
        [SerializeField] int _defaultJumpCount = 2;
        [SerializeField] float _power = 15;
        [SerializeField] Vector2 _wallPower = new Vector2(20, 25);

        public float Power => _power;
        public Vector2 WallPower => _wallPower;

        public int CurrentJumpCount { get; private set; }

        public void CallbackJumpCount()
        {
            CurrentJumpCount--;
        }

        public void InitalizeJumpCount()
        {
            CurrentJumpCount = _defaultJumpCount - 1;
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

    Vector2 _moveDirection = Vector2.zero;

    public JumpData Jump => _jumpData;
    public RigidOperator Rigid => _rigidOperator;
    public StatusData Status { get; private set; } = new StatusData();

    public Vector2 ReadMoveDirection => _moveDirection.normalized;
    public Vector2 SetMoveDirection { set { _moveDirection = value; } }
}
