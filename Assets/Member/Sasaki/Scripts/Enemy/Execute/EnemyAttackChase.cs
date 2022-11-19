using UnityEngine;
using EnemyAISystem;

public class EnemyAttackChase : IEnemyAttack
{
    [SerializeField] float _updateSpeed = 1;

    EnemyAttackCollider _enemyAttackCollider;

    Transform _user;
    Transform _player;

    float IEnemyAttack.AttributeSpeed => _updateSpeed;
    float IEnemyAttack.IsAttackTime => 1;
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
