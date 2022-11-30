using UnityEngine;
using EnemyAISystem;

public class EnemyAttackBoss : IEnemyAttack
{
    [SerializeField] float _updateSpeed;

    EnemyAttackCollider _enemyAttackCollider;

    public float AttributeSpeed => _updateSpeed;

    public float IsAttackTime => 1;

    public float ColliderIsActiveTime => 1;

    public float ColliderActiveTime => 2;

    public EnemyAttackCollider AttackCollider => _enemyAttackCollider;

    public void Setup(Transform user)
    {
        _enemyAttackCollider = user.transform.GetComponentInChildren<EnemyAttackCollider>();
    }

    public Vector2 OnMove()
    {
        return Vector2.right * -1;
    }

    public void Initalize()
    {
        
    }
}
