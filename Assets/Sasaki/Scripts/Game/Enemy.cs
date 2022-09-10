using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy‚Ì§ŒäƒNƒ‰ƒX
/// </summary>

public class Enemy : CharaBase
{
    protected override void Setup()
    {
        
    }

    void Update()
    {
        
        PhysicsOperator.Move(Vector2.zero);
    }
}
