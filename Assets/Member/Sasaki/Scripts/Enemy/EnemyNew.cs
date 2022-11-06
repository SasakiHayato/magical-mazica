using UnityEngine;
using EnemyAISystem;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyNew : MonoBehaviour,IDamagable
{
    [SerializeField] int _speed;
    [SerializeField] int _hp;
    [SerializeField] EnemyAI _enemyAI;
    
    Rigidbody2D _rb;

    Vector2 _beforePosition;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _enemyAI.Setup(transform);
        _beforePosition = transform.position;
    }

    void Update()
    {
        _enemyAI.Process();

        Move();
        Rotate();
    }

    void Move()
    {
        Vector2 move = _enemyAI.MoveDir * _speed;
        move.y = Physics2D.gravity.y;

        _rb.velocity = move;
    }

    /// <summary>
    /// スケールをいじって回転を表現
    /// </summary>
    void Rotate()
    {
        Vector2 forward = (transform.position.Collect() - _beforePosition).normalized;
        forward.y = 1;
        _beforePosition = transform.position;
        
        if (Mathf.Abs(forward.x) > 0.01f)
        {
            forward.x = Mathf.Sign(forward.x) * 1;
            transform.localScale = forward;
        }
    }

    public void AddDamage(int damage)
    {
        _hp -= damage;
        print(_hp);
    }
}
