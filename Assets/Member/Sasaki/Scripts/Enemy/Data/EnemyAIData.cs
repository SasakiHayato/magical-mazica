using UnityEngine;
using MonoState.Data;

public interface IEnemyAIExecutable
{
    void Setup(Transform user);
    Vector2 OnMove();
    void Initalize();
}

public interface IEnemyIdle : IEnemyAIExecutable
{
}

public interface IEnemyMove : IEnemyAIExecutable
{
    float AttributeSpeed { get; }
}

public interface IEnemyAttack : IEnemyAIExecutable
{
}

[System.Serializable]
public class EnemyAIData : IMonoDatableSystem<EnemyAIData>
{
    EnemyAIData IMonoDatableSystem<EnemyAIData>.GetData => this;
    string IMonoDatable.Path => nameof(EnemyAIData);

    [SerializeField] EnemyIdleData _idleData;
    [SerializeField] EnemyMoveData _moveData;
    [SerializeField] EnemyAttackData _attackData;

    public EnemyIdleData IdleData => _idleData;
    public EnemyMoveData MoveData => _moveData;
    public EnemyAttackData AttackData => _attackData;
}

public class EnemyDataBase
{
    [SerializeField] float _attributeDistance;
    public float AttributeDistance => _attributeDistance;
}

[System.Serializable]
public class EnemyIdleData : EnemyDataBase
{
    
    [SerializeReference, SubclassSelector] IEnemyIdle _idle;
    public IEnemyIdle Idle => _idle;
}

[System.Serializable]
public class EnemyMoveData : EnemyDataBase
{
    [SerializeReference, SubclassSelector] IEnemyMove _move;
    public IEnemyMove Move => _move;
}

[System.Serializable]
public class EnemyAttackData : EnemyDataBase
{
    [SerializeReference, SubclassSelector] IEnemyAttack _attack;
    public IEnemyAttack Attack => _attack;
}
