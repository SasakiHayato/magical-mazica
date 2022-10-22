using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExecuteData
{
    public abstract class DataBase
    {
        [SerializeField] int _priority;
        [SerializeField] string _animStateName;
        [SerializeField] float _attributeDist;

        public int Priority => _priority;
        public string AnimStateName => _animStateName;
        public float AttributeDist => _attributeDist;
        public  abstract string Path { get; protected set; }
    }

    [System.Serializable]
    public class OnIdleData : DataBase
    {
        [SerializeReference, SubclassSelector] IExecuteIdle _execute;

        public IExecuteIdle ExecuteIdle => _execute;
        public override string Path { get; protected set; } = "Idle";
    }

    [System.Serializable]
    public class OnMoveData : DataBase
    {
        [SerializeReference, SubclassSelector] IExecuteMove _execute;

        public IExecuteMove ExecuteMove => _execute;
        public override string Path { get; protected set; } = "Move";
    }

    [System.Serializable]
    public class OnAttackData : DataBase
    {
        [SerializeReference, SubclassSelector] IExecuteAttack _execute;

        public IExecuteAttack ExecuteAttack => _execute;
        public override string Path { get; protected set; } = "Attack";
    }

    [SerializeField] OnIdleData _idleData;
    [SerializeField] OnMoveData _moveData;
    [SerializeField] OnAttackData _attackData;

    public OnIdleData IdleData => _idleData;
    public OnMoveData MoveData => _moveData;
    public OnAttackData AttackData => _attackData;
}
