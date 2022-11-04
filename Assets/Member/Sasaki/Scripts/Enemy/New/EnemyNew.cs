using UnityEngine;
using EnemyAISystem;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyNew : MonoBehaviour,IDamagable
{
    [SerializeField] int _speed;
    [SerializeField] int _hp;
    
    EnemyAI _enemyAI;
    Rigidbody2D _rb;

    void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _rb = GetComponent<Rigidbody2D>();

        _enemyAI.Setup(transform);
    }

    void Update()
    {
        _enemyAI.Process();

        Vector2 move = _enemyAI.MoveDir * _speed;
        move.y = Physics2D.gravity.y;

        _rb.velocity = move;
    }

    public void AddDamage(int damage)
    {
        _hp -= damage;
        print(_hp);
    }
}
