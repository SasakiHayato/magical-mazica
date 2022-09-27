using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの制御クラス
/// </summary>

public class Enemy : CharaBase
{
    protected override void Setup()
    {
        
    }

    void Update()
    {
        PhysicsOperator.Move(Vector2.zero);

        if (Input.GetButtonDown("Fire1"))
        {
            PhysicsOperator.Force(Vector2.up * 10, CustomPhysics.Data.ForceType.VerticalThrow);
        }
    }
}
