using UnityEngine;
using EnemyAISystem;

public class EnemyAttackChase : IEnemyAttack
{
    [SerializeField] float _isAttackTime = 1;
    [SerializeField] float _updateSpeed = 1;
    [SerializeField] float _colliderActiveTime = 1;

    EnemyAttackCollider _enemyAttackCollider;

    Transform _user;
    Transform _player;

    float IEnemyAttack.AttributeSpeed => _updateSpeed;
    float IEnemyAttack.IsAttackTime => _isAttackTime;
    float IEnemyAttack.ColliderIsActiveTime => 0;
    float IEnemyAttack.ColliderActiveTime => _colliderActiveTime;
    EnemyAttackCollider IEnemyAttack.AttackCollider => _enemyAttackCollider;

    

    public void Setup(Transform user)
    {
        _enemyAttackCollider = user.GetComponentInChildren<EnemyAttackCollider>();
        _user = user;
        _player = GameController.Instance.Player;
    }

    public Vector2 OnMove()
    {
        return (_player.position - _user.position).normalized;
    }
    
    public void Initalize()
    {
        
    }
}
