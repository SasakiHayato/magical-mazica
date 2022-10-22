using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int _speed;
    [SerializeField] EnemyAIOperator _operator;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 move = _operator.MoveDir * _speed;
        move.y = Physics2D.gravity.y;

        _rb.velocity = move;
    }
}
