using UnityEngine;
using MonoState.Data;
using System.Collections.Generic;

namespace EnemyAISystem
{
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
        float AttributeSpeed { get; }
        float IsAttackTime { get; }
        float ColliderIsActiveTime { get; }
        float ColliderActiveTime { get; }
        EnemyAttackCollider AttackCollider { get; }
    }

    public interface IEnemyAttackEvent
    {
        void Setup(Transform user);
        void EnableEvent();
        void ExecuteEvent();
        void EndEvent();
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
        [SerializeField] float _attackWaitTime = 1;
        [SerializeReference, SubclassSelector] IEnemyAttack _attack;
        [SerializeReference, SubclassSelector] List<IEnemyAttackEvent> _attackEventList;
        public float AttackWaitTime => _attackWaitTime;
        public IEnemyAttack Attack => _attack;
        public List<IEnemyAttackEvent> AttackEvent => _attackEventList;
    }
}