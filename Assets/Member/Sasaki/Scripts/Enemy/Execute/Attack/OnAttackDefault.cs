using UnityEngine;
using EnemyAISystem;

public class OnAttackDefault : IMove, IAttack 
{
    [SerializeField] int _attackIndex;

    void IMove.Setup(Transform user) { }

    Vector2 IMove.Execute() => Vector2.zero;

    void IMove.Initalize() { }

    int IAttack.SendAttackIndex() => _attackIndex;
}
