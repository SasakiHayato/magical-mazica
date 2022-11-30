using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAISystem;

public class EnemyIdleBoss : IEnemyIdle
{
    [SerializeField] float _moveRate = 0.5f;

    public void Setup(Transform user)
    {
        
    }

    public Vector2 OnMove()
    {
        return Vector2.right * -1 * _moveRate;
    }

    public void Initalize()
    {
        
    }
}
