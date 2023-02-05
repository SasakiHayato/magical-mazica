using UnityEngine;
using MonoState.Data;

[System.Serializable]
public class Boss_NewData : IMonoDatableSystem<Boss_NewData>
{
    [System.Serializable]
    public class TouchData
    {
        [SerializeField] int _damage;
        [SerializeField] Vector2 _forceDirection;

        public int Damage => _damage;
        public Vector2 ForceDirection => _forceDirection;
    }

    [System.Serializable]
    public class DistanceData
    {
        [SerializeField] Transform _coreOffset;
        [SerializeField] float _far;
        [SerializeField] float _near;

        public Transform Core => _coreOffset;
        public float Far => _far;
        public float Near => _near;
    }

    [System.Serializable]
    public class MoveSpeedData
    {
        [SerializeField] float _fast;
        [SerializeField] float _srow;

        public float Fast => _fast;
        public float Srow => _srow;
    }

    [System.Serializable]
    public class AttackData
    {
        [SerializeField] Bullet _bulletPrefab;
        [SerializeField] float _bulletSpeed;
        [SerializeField] int _defaultDamage;
        [SerializeField] int _bulletDmage;

        public Bullet BulletPrefab => _bulletPrefab;
        public float BulletSpeed => _bulletSpeed;
        public int DefaultDamage => _defaultDamage;
        public int BulletDamage => _bulletDmage;
    }

    [SerializeField] TouchData _touchData;
    [SerializeField] DistanceData _distanceData;
    [SerializeField] MoveSpeedData _moveSpeedData;
    [SerializeField] AttackData _attackData;

    bool _onAttack = false;
    (int, int) _attackFrame = (0, 0);

    EnemyAttackCollider _attackCollider = null;

    public TouchData Touch => _touchData;
    public DistanceData Distance => _distanceData;
    public MoveSpeedData MoveSpeed => _moveSpeedData;
    public AttackData Attack => _attackData;

    public bool ReadOnAttack => _onAttack;
    public bool SetOnAttack { set { _onAttack = value; } }
    public (int, int) ReadAttackFrame => _attackFrame;

    public EnemyAttackCollider ReadCollider => _attackCollider;
    public EnemyAttackCollider SetCollider { set { _attackCollider = value; } }

    public void SetAttackFrame(int active, int disActive)
    {
        _attackFrame.Item1 = active;
        _attackFrame.Item2 = disActive;
    }

    Boss_NewData IMonoDatableSystem<Boss_NewData>.GetData => this;

    string IMonoDatable.Path => nameof(Boss_NewData);
}
